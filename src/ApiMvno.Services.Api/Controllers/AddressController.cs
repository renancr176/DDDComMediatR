using ApiMvno.Application.Models;
using ApiMvno.Application.Queries.Interfaces;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Services.Api.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiMvno.Services.Api.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
public class AddressController : BaseController
{
    private readonly IAddressQuery _addressQuery;

    public AddressController(INotificationHandler<DomainNotification> notifications, IMediator mediatorHandler,
        IHttpContextAccessor httpContextAccessor, IAddressQuery addressQuery) : base(notifications, mediatorHandler,
        httpContextAccessor)
    {
        _addressQuery = addressQuery;
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [SwaggerResponse(200, Type = typeof(BaseResponse<AddressModel?>))]
    [SwaggerResponse(400, Type = typeof(BaseResponse))]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        return Response(await _addressQuery.GetByIdAsync(id));
    }

    [HttpGet("Types")]
    [AllowAnonymous]
    [SwaggerResponse(200, Type = typeof(BaseResponse<IEnumerable<AddressTypeModel>?>))]
    [SwaggerResponse(400, Type = typeof(BaseResponse))]
    public async Task<IActionResult> GetAllAddressTypesAsync()
    {
        return Response(await _addressQuery.GetAllAddressTypesAsync());
    }
}