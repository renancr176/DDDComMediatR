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

public class PhoneQuery : IPhoneQuery
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IPhoneTypeRepository _phoneTypeRepository;
    private readonly IPhoneRepository _phoneRepository;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PhoneQuery(IMediator mediator, IMapper mapper, IPhoneTypeRepository phoneTypeRepository,
        IPhoneRepository phoneRepository, IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _phoneTypeRepository = phoneTypeRepository;
        _phoneRepository = phoneRepository;
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    #region Consts

    public const string InternalServerError = "An internal server error occurred, try again later.";

    #endregion

    public async Task<IEnumerable<PhoneTypeModel>?> GetAllPhoneTypesAsync()
    {
        try
        {
            var isAdmin = await _userService.CurrentUserHasRole(RoleEnum.Admin);

            return _mapper.Map<IEnumerable<PhoneTypeModel>?>(await _phoneTypeRepository.FindAsync(pt => isAdmin || pt.Active));
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }

    public async Task<PhoneModel?> GetByIdAsync(Guid id)
    {
        try
        {
            return _mapper.Map<PhoneModel?>(await _phoneRepository.GetByIdAsync(id));
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }
}