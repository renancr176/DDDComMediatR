using ApiMvno.Application.Commands.CompanyCommands;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ApiMvno.Application.Commands.UserCommands;

public class UserAddRoleCommandHandler : IRequestHandler<UserAddRoleCommand, bool>
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public UserAddRoleCommandHandler(IMediator mediator, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _mediator = mediator;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    #region Consts

    const string InternalServerError = "An internal server error has occurred, please try again later.";
    const string UserNotFound = "User not found";

    #endregion

    public async Task<bool> Handle(UserAddRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                await _mediator.Publish(new DomainNotification(nameof(UserNotFound), UserNotFound));
                return false;
            }

            foreach (var role in request.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _mediator.Publish(new DomainNotification("RoleNotFound", @$"Role ""{role}"" not found."));
                    return false;
                }
            }

            foreach (var role in request.Roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return true;
        }
        catch (Exception)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return false;
    }
}