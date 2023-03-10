using System.Linq.Expressions;
using ApiMvno.Application.Models;
using ApiMvno.Application.Models.Queries.Requests;
using ApiMvno.Application.Models.Queries.Responses;
using ApiMvno.Application.Queries.Interfaces;
using ApiMvno.Application.Services.Interfaces;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApiMvno.Application.Queries;

public class CountryQuery : ICountryQuery
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICountryRepository _countryRepository;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CountryQuery(IMediator mediator, IMapper mapper, ICountryRepository countryRepository,
        IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _countryRepository = countryRepository;
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    #region Consts

    public const string InternalServerError = "An internal server error occurred, try again later.";

    #endregion

    public async Task<CountryModel?> GetByIdAsync(Guid id)
    {
        try
        {
            var isAdmin = await _userService.CurrentUserHasRole(RoleEnum.Admin);

            var country = await _countryRepository.GetByIdAsync(id);

            return _mapper.Map<CountryModel?>(isAdmin || country.Active ? country : null);
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }

    public async Task<IEnumerable<CountryModel>?> GetAllAsync()
    {
        try
        {
            var isAdmin = await _userService.CurrentUserHasRole(RoleEnum.Admin);

            return _mapper.Map<IEnumerable<CountryModel>?>(
                await _countryRepository.FindAsync(lt => isAdmin || lt.Active));
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }

    public async Task<PagedResponse<CountryModel>?> SearchAsync(CountrySearchRequest request)
    {
        var response = new PagedResponse<CountryModel>(request);
        try
        {
            var isAdmin = await _userService.CurrentUserHasRole(RoleEnum.Admin);

            Expression<Func<Country, bool>> predicate = c =>
            (
                (string.IsNullOrEmpty(request.Name) || c.Name.Trim().ToLower().Contains(request.Name.Trim().ToLower()))
                && (string.IsNullOrEmpty(request.PhoneCode) || c.PhoneCode.Trim().ToLower().Contains(request.PhoneCode.Trim().ToLower()))
                && (isAdmin || c.Active)
            );

            await response.SetTotals(_countryRepository, predicate);

            var searchResult = await _countryRepository.GetPagedAsync(
                request.PageIndex,
                request.PageSize,
                predicate);

            response.Data = _mapper.Map<IEnumerable<CountryModel>>(searchResult);
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }
        return response;
    }
}