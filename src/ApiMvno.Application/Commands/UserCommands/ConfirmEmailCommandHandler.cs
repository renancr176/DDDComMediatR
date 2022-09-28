using ApiMvno.Domain.Core.Extensions;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiMvno.Application.Commands.UserCommands;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;
    private readonly IDistributedCache _memoryCache;

    public ConfirmEmailCommandHandler(IMediator mediator, UserManager<User> userManager, IDistributedCache memoryCache)
    {
        _mediator = mediator;
        _userManager = userManager;
        _memoryCache = memoryCache;
    }

    #region Consts

    private const string InternalServerError = "An internal server error has occurred, please try again later.";
    private const string InvalidToken = "Invalid confirmation token.";

    #endregion

    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            User user = null;
            if (!_memoryCache.TryGetValue(request.Token.IsBase64Encoded()
                        ? request.Token.Base64Decode()
                        : request.Token,
                    out user))
            {
                await _mediator.Publish(new DomainNotification(nameof(InvalidToken), InvalidToken));
                return false;
            }

            user = await _userManager.FindByIdAsync(user.Id.ToString());

            user.EmailConfirmed = true;

            await _userManager.UpdateAsync(user);

            return true;
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return false;
    }
}