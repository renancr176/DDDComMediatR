using ApiMvno.Application.Models;
using ApiMvno.Application.Queries.Interfaces;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Services.Api.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiMvno.Services.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
    public class PhoneController : BaseController
    {
        private readonly IPhoneQuery _phoneQuery;

        public PhoneController(INotificationHandler<DomainNotification> notifications, IMediator mediatorHandler,
            IHttpContextAccessor httpContextAccessor, IPhoneQuery phoneQuery) 
            : base(notifications, mediatorHandler, httpContextAccessor)
        {
            _phoneQuery = phoneQuery;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [SwaggerResponse(200, Type = typeof(BaseResponse<PhoneModel?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            return Response(await _phoneQuery.GetByIdAsync(id));
        }

        [HttpGet("Types")]
        [AllowAnonymous]
        [SwaggerResponse(200, Type = typeof(BaseResponse<IEnumerable<PhoneTypeModel>?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetAllPhoneTypesAsync()
        {
            return Response(await _phoneQuery.GetAllPhoneTypesAsync());
        }
    }
}
