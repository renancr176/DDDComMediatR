using ApiMvno.Application.Models;
using ApiMvno.Application.Queries.Interfaces;
using ApiMvno.Application.Services.Interfaces;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApiMvno.Application.Queries;

public class AddressQuery : IAddressQuery
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IAddressRepository _addressRepository;
    private readonly IAddressTypeRepository _addressTypeRepository;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddressQuery(IMediator mediator, IMapper mapper, IAddressRepository addressRepository,
        IAddressTypeRepository addressTypeRepository, IUserService userService,
        IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _addressRepository = addressRepository;
        _addressTypeRepository = addressTypeRepository;
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    #region Consts

    public const string InternalServerError = "An internal server error occurred, try again later.";

    #endregion

    public async Task<AddressModel?> GetByIdAsync(Guid id)
    {
        try
        {
            return _mapper.Map<AddressModel?>(await _addressRepository.GetByIdAsync(id));
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }

    public async Task<IEnumerable<AddressTypeModel>?> GetAllAddressTypesAsync()
    {
        try
        {
            var isAdmin = await _userService.CurrentUserHasRole(RoleEnum.Admin);

            return _mapper.Map<IEnumerable<AddressTypeModel>?>(await _addressTypeRepository.FindAsync(at => isAdmin || at.Active));
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }
}