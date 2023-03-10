using ApiMvno.Application.Commands.CountryCommands;
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
    [Authorize("Bearer", Roles = $"{nameof(RoleEnum.Admin)}")]
    public class CountryController : BaseController
    {
        private readonly IMediator _mediatorHandler;
        private readonly ICountryQuery _countryQuery;

        public CountryController(INotificationHandler<DomainNotification> notifications, IMediator mediatorHandler,
            IHttpContextAccessor httpContextAccessor, ICountryQuery countryQuery) 
            : base(notifications, mediatorHandler, httpContextAccessor)
        {
            _mediatorHandler = mediatorHandler;
            _countryQuery = countryQuery;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [SwaggerResponse(200, Type = typeof(BaseResponse<CountryModel?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            return Response(await _countryQuery.GetByIdAsync(id));
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse(200, Type = typeof(BaseResponse<IEnumerable<CountryModel>?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _countryQuery.GetAllAsync());
        }

        [HttpGet("Search")]
        [AllowAnonymous]
        [SwaggerResponse(200, Type = typeof(BaseResponse<PagedResponse<CountryModel>?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> SearchAsync([FromQuery] CountrySearchRequest request)
        {
            return Response(await _countryQuery.SearchAsync(request));
        }

        [HttpPost]
        [SwaggerResponse(200, Type = typeof(BaseResponse<CountryModel?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> CreateCountryAsync([FromBody] CreateCountryCommand request)
        {
            return Response(await _mediatorHandler.Send(request));
        }

        [HttpPut]
        [SwaggerResponse(200, Type = typeof(BaseResponse<CountryModel?>))]
        [SwaggerResponse(400, Type = typeof(BaseResponse))]
        public async Task<IActionResult> UpdateCountryAsync([FromBody] UpdateCountryCommand request)
        {
            return Response(await _mediatorHandler.Send(request));
        }
    }
}
