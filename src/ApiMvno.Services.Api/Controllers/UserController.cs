using ApiMvno.Application.Commands.UserCommands;
using ApiMvno.Application.Models;
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
    public class UserController : BaseController
    {
        private readonly IMediator _mediatorHandler;

        public UserController(INotificationHandler<DomainNotification> notifications, IMediator mediatorHandler,
            IHttpContextAccessor httpContextAccessor) 
            : base(notifications, mediatorHandler, httpContextAccessor)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("SingIn")]
        [AllowAnonymous]
        [SwaggerResponse(200, Type = typeof(BaseResponse<SingInResponseModel>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> SingInAsync([FromBody] SingInCommand request)
        {
            if (!ModelState.IsValid) return InvalidModelResponse();

            return Response(await _mediatorHandler.Send(request));
        }

        [HttpPost("ConfirmEmail")]
        [AllowAnonymous]
        [SwaggerResponse(200, Type = typeof(BaseResponse))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailCommand request)
        {
            if (!ModelState.IsValid) return InvalidModelResponse();

            return Response(await _mediatorHandler.Send(request));
        }

        [HttpPost("PasswordReset")]
        [AllowAnonymous]
        [SwaggerResponse(200, Type = typeof(BaseResponse<MessageModel>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> PasswordResetAsync([FromBody] PasswordResetCommand request)
        {
            if (!ModelState.IsValid) return InvalidModelResponse();

            return Response(await _mediatorHandler.Send(request));
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        [SwaggerResponse(200, Type = typeof(BaseResponse<MessageModel>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordCommand request)
        {
            if (!ModelState.IsValid) return InvalidModelResponse();

            return Response(await _mediatorHandler.Send(request));
        }

        [HttpPost("SignUp")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<UserModel>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
#if DEBUG
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = false)]
#else
        [ApiExplorerSettings(IgnoreApi = true)]
#endif
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpCommand request)
        {
            if (!ModelState.IsValid) return InvalidModelResponse();

            return Response(await _mediatorHandler.Send(request));
        }

        [HttpPost("IncludeRole")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<UserModel>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
#if DEBUG
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = false)]
#else
        [ApiExplorerSettings(IgnoreApi = true)]
#endif
        public async Task<IActionResult> IncludeRoleAsync([FromBody] UserAddRoleCommand request)
        {
            if (!ModelState.IsValid) return InvalidModelResponse();

            return Response(await _mediatorHandler.Send(request));
        }

        [HttpPost("ChangeStatus")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<UserModel>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
#if DEBUG
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = false)]
#else
        [ApiExplorerSettings(IgnoreApi = true)]
#endif
        public async Task<IActionResult> ChangeStatusAsync([FromBody] UserChangeStatusCommand request)
        {
            if (!ModelState.IsValid) return InvalidModelResponse();

            return Response(await _mediatorHandler.Send(request));
        }
        

    }
}
