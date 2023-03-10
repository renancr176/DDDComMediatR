using System.Security.Claims;
using ApiMvno.Application.Models;
using ApiMvno.Application.Services.Interfaces;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Core.Extensions;
using ApiMvno.Domain.Core.Options;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;


namespace ApiMvno.Application.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;
    private readonly IDistributedCache _memoryCache;
    private readonly IOptions<GeneralOptions> _generalOptions;
    private readonly IUserCompanyRepository _userCompanyRepository;
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;

    public GeneralOptions GeneralOptions => _generalOptions.Value;

    private string _currentUserId => _httpContextAccessor.HttpContext?.User?
        .FindFirstValue(ClaimTypes.NameIdentifier);

    public UserService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager,
        IDistributedCache memoryCache, IOptions<GeneralOptions> generalOptions,
        IUserCompanyRepository userCompanyRepository, IMapper mapper, ICompanyRepository companyRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _memoryCache = memoryCache;
        _generalOptions = generalOptions;
        _userCompanyRepository = userCompanyRepository;
        _mapper = mapper;
        _companyRepository = companyRepository;
    }

    #region Consts

    public const string EmailConfirmationSubject = "Confirmação de e-mail";
    public const string EmailConfirmationBody = @"<p>Olá #Name</p>
<br/>
<p>Para confirmar o seu e-mail, por favor <a href=""#Url/auth/confirmemail/#Token"">Clique aqui<a/>.</p>";

    #endregion

    public async Task<User> CurrentUserAsync()
    {
        return await _userManager.FindByIdAsync(_currentUserId);
    }

    public async Task<User> FindByUserName(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<bool> CurrentUserHasRole(string roleName)
    {
        var user = await CurrentUserAsync();
        if (user is null)
            return false;

        return _httpContextAccessor.HttpContext?.User.IsInRole(roleName) ?? false;
    }

    public async Task<bool> CurrentUserHasRole(RoleEnum role)
    {
        return await CurrentUserHasRole(role.ToString());
    }

    public async Task<bool> CurrentUserHasRoleAnyAsync(Func<RoleEnum?, bool> predicate)
    {
        var user = await CurrentUserAsync();
        if (user is null)
            return false;

        var userRoles = _httpContextAccessor.HttpContext?.User?
            .Claims?.Where(c => c.Type == ClaimTypes.Role && c.Value.ValueExistsInEnum<RoleEnum>())
            ?.Select(c => c.Value.StringToEnum<RoleEnum>())
            ?.Distinct();

        return userRoles != null && userRoles.Any(predicate);
    }

    public async Task<bool> HasRole(Guid userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return false;
        }

        return await _userManager.IsInRoleAsync(user, roleName);
    }

    public async Task<bool> HasRole(Guid userId, RoleEnum role)
    {
        return await HasRole(userId, role.ToString());
    }

    public async Task<bool> SendEmailConfirmationAsync(Guid userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null || user.EmailConfirmed)
            {
                return false;
            }

            var token = Guid.NewGuid().ToString();

            await _memoryCache.SetObjectAsync(token, user, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2)
            });

            var body = EmailConfirmationBody
                .Replace("#Name", user.Name)
                .Replace("#Url", GeneralOptions.FrontUrl)
                .Replace("#Token", token.Base64Encode());

            //TODO: Implements a Mail Service on CrossCutting to send confirmation email link
            //var sendMailResult = await _mailService.SendAsync(new SendMailResquest(user.Email, EmailConfirmationSubject, body));
            var sendMailResult = true;

            return sendMailResult; 
        }
        catch (Exception)
        {
        }

        return false;
    }

    public async Task<IEnumerable<UserCompany>?> CurrentUserCompaniesAsync()
    {
        var user = await CurrentUserAsync();
        if (user == null)
        {
            return new List<UserCompany>();
        }
        return await UserCompaniesAsync(user.Id);
    }

    public async Task<IEnumerable<UserCompanyModel>?> CurrentUserCompaniesWithNameAsync()
    {
        var user = await CurrentUserAsync();
        if (user == null)
        {
            return new List<UserCompanyModel>();
        }
        return await UserCompaniesWithNameAsync(user.Id);
    }

    public async Task<IEnumerable<Guid>> CurrentUserCompanyIdsAsync()
    {
        return (await CurrentUserCompaniesAsync())
            ?.Select(uc => uc.CompanyId)
            ?? new List<Guid>();
    }

    public async Task<IEnumerable<UserCompany>?> UserCompaniesAsync(Guid id)
    {
        return await _userCompanyRepository.FindAsync(uc => uc.UserId == id);
    }

    public async Task<IEnumerable<UserCompanyModel>?> UserCompaniesWithNameAsync(Guid id)
    {
        var userCompanies = _mapper.Map<IEnumerable<UserCompanyModel>>(await UserCompaniesAsync(id));
        foreach (var userCompany in userCompanies)
        {
            userCompany.CompanyName = (await _companyRepository.GetByIdAsync(userCompany.CompanyId))?.Name;
        }
        return userCompanies;
    }

    public async Task<bool> CurrentUserHasCompanyAsync(Guid companyId)
    {
        var user = await CurrentUserAsync();
        if (user == null)
        {
            return false;
        }
        return await UserHasCompanyAsync(user, companyId);
    }

    public async Task<bool> UserHasCompanyAsync(User user, Guid companyId)
    {
        return await _userCompanyRepository.AnyAsync(uc => uc.UserId == user.Id && uc.CompanyId == companyId);
    }
}