using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class UpdateCompanyAddressCommandHandler : IRequestHandler<UpdateCompanyAddressCommand, CompanyAddressModel?>
{

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICompanyAddressRepository _companyAddressRepository;
    private readonly ICompanyAddressValidator _companyAddressValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateCompanyAddressCommandHandler(IMediator mediator, IMapper mapper,
        ICompanyAddressRepository companyAddressRepository, ICompanyAddressValidator companyAddressValidator,
        IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _companyAddressRepository = companyAddressRepository;
        _companyAddressValidator = companyAddressValidator;
        _httpContextAccessor = httpContextAccessor;
    }

    #region Consts

    private const string InternalServerError = "An internal server error has occurred, please try again later.";
    private const string AddressNotExists = "Address not exists.";

    #endregion

    public async Task<CompanyAddressModel?> Handle(UpdateCompanyAddressCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var companyAddress = await _companyAddressRepository.GetByIdAsync(request.Id);

            if (companyAddress is null)
            {
                await _mediator.Publish(new DomainNotification(nameof(AddressNotExists), AddressNotExists));
                return default;
            }
            
            companyAddress = _mapper.Map(request, companyAddress);

            if (!await _companyAddressValidator.IsValidAsync(companyAddress))
            {
                foreach (var error in _companyAddressValidator.ValidationResult.Errors)
                {
                    await _mediator.Publish(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return default;
            }

            await _companyAddressRepository.UpdateAsync(companyAddress);

            if (request.AggregateId == Guid.Empty)
                await _companyAddressRepository.UnitOfWork.Commit();

            return _mapper.Map<CompanyAddressModel>(companyAddress);
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }
}