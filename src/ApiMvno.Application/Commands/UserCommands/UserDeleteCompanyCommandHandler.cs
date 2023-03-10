using ApiMvno.Application.Services.Interfaces;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ApiMvno.Application.Commands.UserCommands;

public class UserDeleteCompanyCommandHandler : 
    IRequestHandler<UserDeleteCompanyCommand, bool>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMediator _mediator;
    private readonly IUserCompanyRepository _userCompanyRepository;
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;

    public UserDeleteCompanyCommandHandler(IHttpContextAccessor httpContextAccessor, IMediator mediator,
        IUserCompanyRepository userCompanyRepository, UserManager<User> userManager, IUserService userService)
    {
        _httpContextAccessor = httpContextAccessor;
        _mediator = mediator;
        _userCompanyRepository = userCompanyRepository;
        _userManager = userManager;
        _userService = userService;
    }

    #region Consts

    const string InternalServerError = "An internal server error has occurred, please try again later.";
    const string UserCompanyNotExists = "This user company configuration does not exists or it was already deleted.";

    #endregion

    public async Task<bool> Handle(UserDeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userCompany = await _userCompanyRepository.GetByIdAsync(request.Id);
            if (userCompany == null)
            {
                await _mediator.Publish(new DomainNotification(nameof(UserCompanyNotExists), UserCompanyNotExists));
            }

            userCompany.DeletedAt = DateTime.UtcNow;
            userCompany.DeletedByUserId = (await _userService.CurrentUserAsync())?.Id;

            await _userCompanyRepository.UpdateAsync(userCompany);

            if (!await _userCompanyRepository.AnyAsync(uc => uc.Id != request.Id && uc.UserId == userCompany.UserId))
            {
                var user = await _userManager.FindByIdAsync(userCompany.UserId.ToString());
                await _userManager.RemoveFromRoleAsync(user, RoleEnum.CompanyAdmin.ToString());
            }

            if (request.AggregateId == Guid.Empty)
                await _userCompanyRepository.UnitOfWork.Commit();

            return true;
        }
        catch (Exception e)
        {
            await _mediator.Publish(new DomainNotification(nameof(InternalServerError), InternalServerError));
        }

        return false;
    }
}