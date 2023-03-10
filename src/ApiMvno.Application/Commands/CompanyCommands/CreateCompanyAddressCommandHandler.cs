using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using AutoMapper;
using MediatR;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class CreateCompanyAddressCommandHandler : IRequestHandler<CreateCompanyAddressCommand, CompanyAddressModel?>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICompanyAddressRepository _companyAddressRepository;
    private readonly ICompanyAddressValidator _companyAddressValidator;

    public CreateCompanyAddressCommandHandler(IMediator mediator, IMapper mapper,
        ICompanyAddressRepository companyAddressRepository, ICompanyAddressValidator companyAddressValidator)
    {
        _mediator = mediator;
        _mapper = mapper;
        _companyAddressRepository = companyAddressRepository;
        _companyAddressValidator = companyAddressValidator;
    }

    #region Consts

    private const string InternalServerError = "An internal server error has occurred, please try again later.";

    #endregion

    public async Task<CompanyAddressModel?> Handle(CreateCompanyAddressCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var companyAddress = _mapper.Map<CompanyAddress>(request);

            if (!await _companyAddressValidator.IsValidAsync(companyAddress))
            {
                foreach (var error in _companyAddressValidator.ValidationResult.Errors)
                {
                    await _mediator.Publish(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return default;
            }

            await _companyAddressRepository.InsertAsync(companyAddress);

            if (request.AggregateId == Guid.Empty)
                await _companyAddressRepository.UnitOfWork.Commit();

            return _mapper.Map<CompanyAddressModel>(companyAddress);
        }
        catch (Exception)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }
}