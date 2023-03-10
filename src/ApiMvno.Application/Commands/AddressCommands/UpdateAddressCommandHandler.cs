using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Interfaces.Repositories;
using ApiMvno.Domain.Interfaces.Validators;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApiMvno.Application.Commands.AddressCommands;

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, AddressModel?>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IAddressRepository _addressRepository;
    private readonly IAddressValidator _addressValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateAddressCommandHandler(IMediator mediator, IMapper mapper, IAddressRepository addressRepository,
        IAddressValidator addressValidator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _addressRepository = addressRepository;
        _addressValidator = addressValidator;
        _httpContextAccessor = httpContextAccessor;
    }

    #region Consts

    public const string InternalServerError = "An internal server error has occurred, please try again later.";
    public const string AddressNotExists = "Address not exists.";

    #endregion

    public async Task<AddressModel?> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var address = await _addressRepository.GetByIdAsync(request.Id);

            if (address is null)
            {
                await _mediator.Publish(new DomainNotification(nameof(AddressNotExists), AddressNotExists));
                return default;
            }

            address = _mapper.Map(request, address);

            if (!await _addressValidator.IsValidAsync(address))
            {
                foreach (var error in _addressValidator.ValidationResult.Errors)
                {
                    await _mediator.Publish(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return default;
            }

            await _addressRepository.UpdateAsync(address);
            await _addressRepository.UnitOfWork.Commit();

            return _mapper.Map<AddressModel>(address);
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }
}
