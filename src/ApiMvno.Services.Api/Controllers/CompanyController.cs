using ApiMvno.Application.Commands.CompanyCommands;
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
    public class CompanyController : BaseController
    {
        private readonly IMediator _mediatorHandler;

        public CompanyController(INotificationHandler<DomainNotification> notifications, IMediator mediatorHandler,
            IHttpContextAccessor httpContextAccessor) 
            : base(notifications, mediatorHandler, httpContextAccessor)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        [SwaggerResponse(200, Type = typeof(BaseResponse<CompanyModel>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyCommand request)
        {
            return Response(await _mediatorHandler.Send(request));
        }

        [HttpPut]
        [SwaggerResponse(200, Type = typeof(BaseResponse<CompanyModel>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCompanyCommand request)
        {
            return Response(await _mediatorHandler.Send(request));
        }
    }
}
