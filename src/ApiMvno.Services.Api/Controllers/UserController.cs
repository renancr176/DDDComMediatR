using ApiMvno.Application.Commands.UserCommands;
using ApiMvno.Application.Models;
using ApiMvno.Application.Services.Interfaces;
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
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IMediator _mediatorHandler;
        private readonly IUserService _userService;

        public UserController(INotificationHandler<DomainNotification> notifications, IMediator mediatorHandler,
            IHttpContextAccessor httpContextAccessor, IUserService userService) 
            : base(notifications, mediatorHandler,
            httpContextAccessor)
        {
            _mediatorHandler = mediatorHandler;
            _userService = userService;
        }

        [HttpPost("SignIn")]
        [AllowAnonymous]
        [SwaggerResponse(200, Type = typeof(BaseResponse<SignInResponseModel>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> SignInAsync([FromBody] SignInCommand request)
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
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
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
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
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
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
        [ApiExplorerSettings(IgnoreApi = true)]
#endif
        public async Task<IActionResult> ChangeStatusAsync([FromBody] UserChangeStatusCommand request)
        {
            if (!ModelState.IsValid) return InvalidModelResponse();

            return Response(await _mediatorHandler.Send(request));
        }

        [HttpGet("Companies")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<IEnumerable<UserCompanyModel>?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
#if DEBUG
        [ApiExplorerSettings(IgnoreApi = false)]
#else
        [ApiExplorerSettings(IgnoreApi = true)]
#endif
        public async Task<IActionResult> CurrentUserCompaniesAsync()
        {
            return Response(await _userService.CurrentUserCompaniesWithNameAsync());
        }

        [HttpGet("{id:guid}/Companies")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<IEnumerable<UserCompanyModel>?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
#if DEBUG
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = false)]
#else
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
        [ApiExplorerSettings(IgnoreApi = true)]
#endif
        public async Task<IActionResult> UserCompaniesAsync(Guid id)
        {
            return Response(await _userService.UserCompaniesWithNameAsync(id));
        }
        
        [HttpPost("Company")]
        [SwaggerResponse(200, Type = typeof(BaseResponse))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
#if DEBUG
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = false)]
#else
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
        [ApiExplorerSettings(IgnoreApi = true)]
#endif
        public async Task<IActionResult> AddUserCompanyAsync([FromBody] UserAddCompanyCommand request)
        {
            if (!ModelState.IsValid) return InvalidModelResponse();

            return Response(await _mediatorHandler.Send(request));
        }

        [HttpDelete("Company")]
        [SwaggerResponse(200, Type = typeof(BaseResponse))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
#if DEBUG
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = false)]
#else
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
        [ApiExplorerSettings(IgnoreApi = true)]
#endif
        public async Task<IActionResult> DeleteUserCompanyAsync([FromBody] UserDeleteCompanyCommand request)
        {
            if (!ModelState.IsValid) return InvalidModelResponse();

            return Response(await _mediatorHandler.Send(request));
        }
    }
}
