using System.Text.RegularExpressions;
using ApiMvno.Application.Models;
using ApiMvno.Application.Services.Interfaces;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ApiMvno.Application.Commands.UserCommands;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, UserModel>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;

    public SignUpCommandHandler(IMediator mediator, IMapper mapper, UserManager<User> userManager, IUserService userService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userManager = userManager;
        _userService = userService;
    }

    #region Consts

    const string InternalServerError = "An internal server error has occurred, please try again later.";

    #endregion

    public async Task<UserModel> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Password)
                || !Regex.IsMatch(request.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%¨&*_+-=^~?<>]).{8,50}$"))
            {
                await _mediator.Publish(new DomainNotification("InvalidPassword", "Password pattern not met."));
                return default!;
            }

            var user = _mapper.Map<User>(request);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    await _mediator.Publish(new DomainNotification(identityError.Code, identityError.Description));
                }
                return default!;
            }

            if (request.Roles != null && request.Roles.Any())
            {
                if (!await _mediator.Send(new UserAddRoleCommand(user.Id, request.Roles.Select(e => e.ToString()))))
                {
                    return default!;
                }
            }

            if (request.Roles == null || !request.Roles.Contains(RoleEnum.Customer))
            {
                await _userService.SendEmailConfirmationAsync(user.Id);
            }

            var userModel = _mapper.Map<UserModel>(user);
            userModel.Roles = request.Roles?.Select(e => e.ToString());

            return userModel;
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return default!;
    }
}