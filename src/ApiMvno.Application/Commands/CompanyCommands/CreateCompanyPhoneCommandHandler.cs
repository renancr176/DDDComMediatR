using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using AutoMapper;
using MediatR;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class CreateCompanyPhoneCommandHandler : IRequestHandler<CreateCompanyPhoneCommand, CompanyPhoneModel?>
{

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICompanyPhoneRepository _companyPhoneRepository;
    private readonly ICompanyPhoneValidator _companyPhoneValidator;

    public CreateCompanyPhoneCommandHandler(IMediator mediator, IMapper mapper, ICompanyPhoneRepository companyPhoneRepository, ICompanyPhoneValidator companyPhoneValidator)
    {
        _mediator = mediator;
        _mapper = mapper;
        _companyPhoneRepository = companyPhoneRepository;
        _companyPhoneValidator = companyPhoneValidator;
    }

    #region Consts

    private const string InternalServerError = "An internal server error has occurred, please try again later.";

    #endregion
    
    public async Task<CompanyPhoneModel?> Handle(CreateCompanyPhoneCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var companyPhone = _mapper.Map<CompanyPhone>(request);

            if (!await _companyPhoneValidator.IsValidAsync(companyPhone))
            {
                foreach (var error in _companyPhoneValidator.ValidationResult.Errors)
                {
                    await _mediator.Publish(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return default;
            }

            await _companyPhoneRepository.InsertAsync(companyPhone);

            if (request.AggregateId == Guid.Empty)
                await _companyPhoneRepository.UnitOfWork.Commit();

            return _mapper.Map<CompanyPhoneModel>(companyPhone);
        }
        catch (Exception)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }
}