using ApiMvno.Application.Services.Interfaces;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Core.Messages.CommonMessages.Notifications;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ApiMvno.Application.Commands.UserCommands;

public class UserAddCompanyCommandHandler
    : IRequestHandler<UserAddCompanyCommand, bool>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IUserService _userService;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserCompanyRepository _userCompanyRepository;

    public UserAddCompanyCommandHandler(IHttpContextAccessor httpContextAccessor, IMediator mediator,
        UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IUserService userService,
        ICompanyRepository companyRepository, IUserCompanyRepository userCompanyRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _mediator = mediator;
        _userManager = userManager;
        _roleManager = roleManager;
        _userService = userService;
        _companyRepository = companyRepository;
        _userCompanyRepository = userCompanyRepository;
    }

    #region Consts

    const string InternalServerError = "An internal server error has occurred, please try again later.";
    const string UserNotExists = "User not exists.";
    const string CompanyNotFound = "One or more informed companies were not found.";

    #endregion

    public async Task<bool> Handle(UserAddCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                await _mediator.Publish(new DomainNotification(nameof(UserNotExists), UserNotExists));
            }

            if (!await _companyRepository.AllAsync(c => request.CompaniesId.Contains(c.Id)))
            {
                await _mediator.Publish(new DomainNotification(nameof(CompanyNotFound), CompanyNotFound));
            }

            foreach (var companyId in request.CompaniesId)
            {
                if (!await _userCompanyRepository.AnyAsync(uc => uc.UserId == user.Id && uc.CompanyId == companyId))
                {
                    await _userCompanyRepository.InsertAsync(new UserCompany(user.Id, companyId)
                    {
                        CreateByUserId = (await _userService.CurrentUserAsync()).Id
                    });

                    if (!await _userService.HasRole(user.Id, RoleEnum.CompanyAdmin))
                    {
                        await _userManager.AddToRoleAsync(user, RoleEnum.CompanyAdmin.ToString());
                    }
                }
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