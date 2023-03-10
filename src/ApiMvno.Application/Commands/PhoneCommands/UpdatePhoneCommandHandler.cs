using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApiMvno.Application.Commands.PhoneCommands;

public class UpdatePhoneCommandHandler 
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IPhoneRepository _phoneRepository;
    private readonly IPhoneValidator _phoneValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdatePhoneCommandHandler(IMediator mediator, IMapper mapper, IPhoneRepository phoneRepository,
        IPhoneValidator phoneValidator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _phoneRepository = phoneRepository;
        _phoneValidator = phoneValidator;
        _httpContextAccessor = httpContextAccessor;
    }

    #region Consts

    public const string InternalServerError = "An internal server error has occurred, please try again later.";
    public const string AddressNotExists = "Address not exists.";

    #endregion

    public async Task<PhoneModel?> Handle(UpdatePhoneCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var address = await _phoneRepository.GetByIdAsync(request.Id);

            if (address is null)
            {
                await _mediator.Publish(new DomainNotification(nameof(AddressNotExists), AddressNotExists));
                return default;
            }

            address = _mapper.Map(request, address);

            if (!await _phoneValidator.IsValidAsync(address))
            {
                foreach (var error in _phoneValidator.ValidationResult.Errors)
                {
                    await _mediator.Publish(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return default;
            }

            await _phoneRepository.UpdateAsync(address);
            await _phoneRepository.UnitOfWork.Commit();

            return _mapper.Map<PhoneModel>(address);
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }
}
