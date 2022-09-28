using System.Security.Claims;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Core.Extensions;
using ApiMvno.Domain.Core.Options;
using ApiMvno.Domain.Entities;
using ApiMvno.Infra.CrossCutting.Mail;
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
    private readonly IMailService _mailService;
    private readonly IOptions<GeneralOptions> _generalOptions;

    public GeneralOptions GeneralOptions => _generalOptions.Value;

    private string _currentUserId => _httpContextAccessor.HttpContext?.User?
        .FindFirstValue(ClaimTypes.NameIdentifier);

    public UserService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager,
        IDistributedCache memoryCache, IMailService mailService, IOptions<GeneralOptions> generalOptions)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _memoryCache = memoryCache;
        _mailService = mailService;
        _generalOptions = generalOptions;
    }

    #region Consts

    private const string EmailConfirmationSubject = "Confirmação de e-mail";
    private const string EmailConfirmationBody = @"<p>Olá #Name</p>
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

            return await _mailService.SendAsync(new SendMailResquest(user.Email, EmailConfirmationSubject, body));
        }
        catch (Exception)
        {
        }

        return false;
    }
}