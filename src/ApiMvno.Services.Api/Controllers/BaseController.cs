using System.Security.Claims;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Services.Api.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiMvno.Services.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediator _mediatorHandler;

        protected Guid ClienteId;

        protected BaseController(INotificationHandler<DomainNotification> notifications,
            IMediator mediatorHandler,
            IHttpContextAccessor httpContextAccessor)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;

            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated) return;

            var claim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            ClienteId = Guid.Parse(claim.Value);
        }

        protected bool ValidOperation()
        {
            return !_notifications.HasNotification();
        }

        protected List<BaseResponseError> GetErrorMessages()
        {
            return _notifications.GetNotifications().Select(c => new BaseResponseError()
            {
                ErrorCode = c.Key,
                Message = c.Value
            })
            .ToList();
        }

        protected void NotificarErro(string errorCode, string errorMessage)
        {
            _mediatorHandler.Publish(new DomainNotification(errorCode, errorMessage));
        }

        protected new IActionResult Response(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(new BaseResponse<object>()
                {
                    Data = result
                });
            }

            return BadRequest(new BaseResponse<object>()
            {
                Errors = GetErrorMessages()
            });
        }

        protected IActionResult InvalidModelResponse()
        {
            return Response(new BaseResponse()
            {
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => new BaseResponseError()
                {
                    ErrorCode = "ModelError",
                    Message = e.ErrorMessage
                }).ToList()
            });
        }
    }
}
