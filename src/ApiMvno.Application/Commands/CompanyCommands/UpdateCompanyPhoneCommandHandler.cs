using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class UpdateCompanyPhoneCommandHandler : IRequestHandler<UpdateCompanyPhoneCommand, CompanyPhoneModel?>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICompanyPhoneRepository _companyPhoneRepository;
    private readonly ICompanyPhoneValidator _companyPhoneValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateCompanyPhoneCommandHandler(IMediator mediator, IMapper mapper,
        ICompanyPhoneRepository companyPhoneRepository, ICompanyPhoneValidator companyPhoneValidator,
        IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _companyPhoneRepository = companyPhoneRepository;
        _companyPhoneValidator = companyPhoneValidator;
        _httpContextAccessor = httpContextAccessor;
    }

    #region Consts

    private const string InternalServerError = "An internal server error has occurred, please try again later.";
    private const string PhoneNotExists = "Phone not exists.";

    #endregion

    public async Task<CompanyPhoneModel?> Handle(UpdateCompanyPhoneCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var companyPhone = await _companyPhoneRepository.GetByIdAsync(request.Id);

            if (companyPhone is null)
            {
                await _mediator.Publish(new DomainNotification(nameof(PhoneNotExists), PhoneNotExists));
                return default;
            }

            companyPhone = _mapper.Map(request, companyPhone);

            if (!await _companyPhoneValidator.IsValidAsync(companyPhone))
            {
                foreach (var error in _companyPhoneValidator.ValidationResult.Errors)
                {
                    await _mediator.Publish(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return default!;
            }

            await _companyPhoneRepository.UpdateAsync(companyPhone);

            if (request.AggregateId == Guid.Empty)
                await _companyPhoneRepository.UnitOfWork.Commit();

            return _mapper.Map<CompanyPhoneModel>(companyPhone);
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }
}