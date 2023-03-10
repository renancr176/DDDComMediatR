using ApiMvno.Application.Commands.CompanyCommands;
using ApiMvno.Application.Models;
using ApiMvno.Application.Models.Queries.Requests;
using ApiMvno.Application.Models.Queries.Responses;
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
    public class CompanyController : BaseController
    {
        private readonly IMediator _mediatorHandler;
        private readonly ICompanyQuery _companyQuery;

        public CompanyController(INotificationHandler<DomainNotification> notifications, IMediator mediatorHandler,
            IHttpContextAccessor httpContextAccessor, ICompanyQuery companyQuery) 
            : base(notifications, mediatorHandler, httpContextAccessor)
        {
            _mediatorHandler = mediatorHandler;
            _companyQuery = companyQuery;
        }

        [HttpGet("{id:guid}")]
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)},{nameof(RoleEnum.BackService)}")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<CompanyModel>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Response(await _companyQuery.GetByIdAsync(id));
        }

        [HttpGet("Search")]
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)},{nameof(RoleEnum.BackService)}")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<PagedResponse<CompanyModel>>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> SearchAsync([FromQuery] CompanySearchRequest request)
        {
            if (!ModelState.IsValid) return InvalidModelResponse();

            return Response(await _companyQuery.SearchAsync(request));
        }

        [HttpPost]
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<CompanyModel?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyCommand request)
        {
            return Response(await _mediatorHandler.Send(request));
        }

        [HttpPut]
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<CompanyModel?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCompanyCommand request)
        {
            return Response(await _mediatorHandler.Send(request));
        }

        [HttpPut("Address")]
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<CompanyAddressModel?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> UpdateCompanyAddressAsync([FromBody] UpdateCompanyAddressCommand request)
        {
            return Response(await _mediatorHandler.Send(request));
        }

        [HttpGet("{id:guid}/Addresses")]
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)},{nameof(RoleEnum.BackService)}")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<IEnumerable<CompanyAddressModel>?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetAllAddressesByCompanyIdAsync([FromRoute] Guid id)
        {
            return Response(await _companyQuery.GetAllAddressesByCompanyIdAsync(id));
        }

        [HttpPut("Phone")]
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<CompanyPhoneModel?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> UpdateCompanyPhoneAsync([FromBody] UpdateCompanyPhoneCommand request)
        {
            return Response(await _mediatorHandler.Send(request));
        }

        [HttpGet("{id:guid}/Phones")]
        [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)},{nameof(RoleEnum.BackService)}")]
        [SwaggerResponse(200, Type = typeof(BaseResponse<IEnumerable<CompanyPhoneModel>?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetAllPhonesByCompanyIdAsync([FromRoute] Guid id)
        {
            return Response(await _companyQuery.GetAllPhonesByCompanyIdAsync(id));
        }
    }
}
