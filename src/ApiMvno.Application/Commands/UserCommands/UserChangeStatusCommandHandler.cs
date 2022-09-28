using ApiMvno.Application.Services;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ApiMvno.Application.Commands.UserCommands;

public class UserChangeStatusCommandHandler : IRequestHandler<UserChangeStatusCommand, bool>
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;

    public UserChangeStatusCommandHandler(IMediator mediator, UserManager<User> userManager, IUserService userService)
    {
        _mediator = mediator;
        _userManager = userManager;
        _userService = userService;
    }

    #region Consts

    private const string InternalServerError = "An internal server error has occurred, please try again later.";
    private const string UserNotFound = "User not found";
    private const string UserCannotChangeOwnStatus = "User cannot change own status.";
    private const string CannotChangeCustomersStatus = "Cannot change customers status.";

    #endregion

    public async Task<bool> Handle(UserChangeStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var currentUser = await _userService.CurrentUserAsync();
            if (currentUser != null && currentUser.Id == request.UserId)
            {
                await _mediator.Publish(new DomainNotification(nameof(UserCannotChangeOwnStatus), UserCannotChangeOwnStatus));
                return false;
            }

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                await _mediator.Publish(new DomainNotification(nameof(UserNotFound), UserNotFound));
                return false;
            }

            if (await _userManager.IsInRoleAsync(user, RoleEnum.Customer.ToString()))
            {
                await _mediator.Publish(new DomainNotification(nameof(CannotChangeCustomersStatus), CannotChangeCustomersStatus));
                return false;
            }

            user.Status = request.Status;

            await _userManager.UpdateAsync(user);
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return false;
    }
}