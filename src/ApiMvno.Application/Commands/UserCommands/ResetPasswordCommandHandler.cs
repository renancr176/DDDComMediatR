using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Extensions;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiMvno.Application.Commands.UserCommands;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, MessageModel>
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;
    private readonly IDistributedCache _memoryCache;

    public ResetPasswordCommandHandler(IMediator mediator, UserManager<User> userManager, IDistributedCache memoryCache)
    {
        _mediator = mediator;
        _userManager = userManager;
        _memoryCache = memoryCache;
    }

    #region Consts

    const string InternalServerError = "An internal server error has occurred, please try again later.";
    const string InvalidToken = "Invalid reset password token.";

    #endregion

    public async Task<MessageModel> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!_memoryCache.TryGetValue(request.Token,
                    out User user)
                || !await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider,
                    "ResetPassword", request.Token))
            {
                await _mediator.Publish(new DomainNotification(nameof(InvalidToken), InvalidToken));
                return default!;
            }

            user = await _userManager.FindByIdAsync(user.Id.ToString());

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    await _mediator.Publish(new DomainNotification(identityError.Code, identityError.Description));
                }
                return default!;
            }

            if (!user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }

            _memoryCache.Remove(request.Token);

            return new MessageModel()
            {
                Message = "Password reseted successfuly."
            };
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default!;
    }
}