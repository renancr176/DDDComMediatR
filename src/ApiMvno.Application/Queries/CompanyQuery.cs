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

public class CompanyQuery : ICompanyQuery
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyPhoneRepository _companyPhoneRepository;
    private readonly ICompanyAddressRepository _companyAddressRepository;

    public CompanyQuery(IMediator mediator, IMapper mapper, IHttpContextAccessor httpContextAccessor,
        IUserService userService, ICompanyRepository companyRepository, ICompanyPhoneRepository companyPhoneRepository,
        ICompanyAddressRepository companyAddressRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
        _companyRepository = companyRepository;
        _companyPhoneRepository = companyPhoneRepository;
        _companyAddressRepository = companyAddressRepository;
    }

    #region Consts

    public const string InternalServerError = "An internal server error occurred, try again later.";

    #endregion

    private IEnumerable<string> Inclues = new[]
    {
        $"{nameof(Company.CompanyAddresses)}.{nameof(CompanyAddress.Address)}",
        $"{nameof(Company.CompanyPhones)}.{nameof(CompanyPhone.Phone)}"
    };

    public async Task<CompanyModel?> GetByIdAsync(Guid id)
    {
        try
        {
            var isAdmin = await _userService.CurrentUserHasRole(RoleEnum.Admin);
            var useCompanyIds = await _userService.CurrentUserCompanyIdsAsync();

            return _mapper.Map<CompanyModel?>(
                await _companyRepository.FirstOrDefaultAsync(c =>
                    c.Id == id
                    && (isAdmin || useCompanyIds.Contains(c.Id)),
                    Inclues)
                );
        }
        catch (Exception)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }

    public async Task<PagedResponse<CompanyModel>?> SearchAsync(CompanySearchRequest request)
    {
        var response = new PagedResponse<CompanyModel>(request);
        try
        {
            var isAdmin = await _userService.CurrentUserHasRole(RoleEnum.Admin);
            var useCompanyIds = await _userService.CurrentUserCompanyIdsAsync();

            Expression<Func<Company, bool>> predicate = company => 
                (
                    (isAdmin || useCompanyIds.Contains(company.Id))
                    && (string.IsNullOrEmpty(request.Email) || company.Email.Trim().ToLower() == request.Email.Trim().ToLower())
                    && (string.IsNullOrEmpty(request.Document) || company.Document == request.Document)
                    && (string.IsNullOrEmpty(request.Name) || company.Name.Trim().ToLower().Contains(request.Name.Trim().ToLower()))
                );

            await response.SetTotals(_companyRepository, predicate);

            var searchResult = await _companyRepository.GetPagedAsync(
                request.PageIndex,
                request.PageSize,
                Inclues,
                predicate);

            response.Data = _mapper.Map<IEnumerable<CompanyModel>>(searchResult);
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }
        return response;
    }

    public async Task<IEnumerable<CompanyPhoneModel>?> GetAllPhonesByCompanyIdAsync(Guid id)
    {
        try
        {
            var isAdmin = await _userService.CurrentUserHasRole(RoleEnum.Admin);
            var useCompanyIds = await _userService.CurrentUserCompanyIdsAsync();

            return _mapper.Map<IEnumerable<CompanyPhoneModel>?>(
                await _companyPhoneRepository.FindAsync(cp =>
                    cp.Id == id
                    && (isAdmin || useCompanyIds.Contains(cp.CompanyId)),
                    new []
                    {
                        nameof(CompanyPhone.Phone)
                    })
                );
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }

    public async Task<IEnumerable<CompanyAddressModel>?> GetAllAddressesByCompanyIdAsync(Guid companyId)
    {
        try
        {
            var isAdmin = await _userService.CurrentUserHasRole(RoleEnum.Admin);
            var useCompanyIds = await _userService.CurrentUserCompanyIdsAsync();

            return _mapper.Map<IEnumerable<CompanyAddressModel>?>(
                await _companyAddressRepository.FindAsync(ca =>
                    ca.CompanyId == companyId
                    && (isAdmin || useCompanyIds.Contains(ca.CompanyId)),
                    new []
                    {
                        $"{nameof(CompanyAddress.Address)}.{nameof(Address.Country)}"
                    })
                );
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default;
    }
}