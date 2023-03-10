using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApiMvno.Application.Models;
using ApiMvno.Application.Services.Interfaces;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Core.Options;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ApiMvno.Application.Commands.UserCommands;

public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInResponseModel>
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IOptions<JwtTokenOptions> _jwtTokenOptions;
    private readonly IUserService _userService;

    public SignInCommandHandler(IMediator mediator, UserManager<User> userManager, SignInManager<User> signInManager,
        IOptions<JwtTokenOptions> jwtTokenOptions, IUserService userService)
    {
        _mediator = mediator;
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenOptions = jwtTokenOptions;
        _userService = userService;
    }

    #region Consts

    public const string LoginBlocked = "Login blocked.";
    public const string InvalidUseramePassword = "Invalid username or password.";
    public const string EmailNotConfirmed = "Unverified email, check your email and follow the instructions for email confirmation.";
    public const string UserDeactivated = "User deactivated.";

    #endregion

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    public async Task<SignInResponseModel> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                await _mediator.Publish(new DomainNotification(nameof(LoginBlocked), LoginBlocked));
            }
            else
            {
                await _mediator.Publish(new DomainNotification(nameof(InvalidUseramePassword), InvalidUseramePassword));
            }

            return default!;
        }

        var user = await _userManager.FindByNameAsync(request.UserName);

        if (!user.EmailConfirmed)
        {
            await _userService.SendEmailConfirmationAsync(user.Id);
            await _mediator.Publish(new DomainNotification(nameof(EmailNotConfirmed), EmailNotConfirmed));
            return default!;
        }

        if (user.Status != UserStatusEnum.Active)
        {
            await _mediator.Publish(new DomainNotification(nameof(UserDeactivated), UserDeactivated));
            return default!;
        }

        var userClaims = await _userManager.GetClaimsAsync(user);

        userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, await _jwtTokenOptions.Value.JtiGenerator()));
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtTokenOptions.Value.IssuedAt).ToString(), ClaimValueTypes.Integer64));

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            userClaims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            userClaims.Add(new Claim("roles", role));
        }

        var jwt = new JwtSecurityToken(
            issuer: _jwtTokenOptions.Value.Issuer,
            audience: _jwtTokenOptions.Value.Audience,
            claims: userClaims,
            notBefore: _jwtTokenOptions.Value.NotBefore,
            expires: _jwtTokenOptions.Value.Expiration,
            signingCredentials: _jwtTokenOptions.Value.SigningCredentials);

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new SignInResponseModel()
        {
            AccessToken = encodedJwt,
            ExpiresIn = _jwtTokenOptions.Value.ValidFor.TotalSeconds,
            User = new UserModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Roles = roles.ToList(),
                Status = user.Status
            }
        };
    }
}