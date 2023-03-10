using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Entities;

namespace ApiMvno.Application.Services.Interfaces;

public interface IUserService
{
    Task<User> CurrentUserAsync();
    Task<User> FindByUserName(string userName);
    Task<bool> CurrentUserHasRole(string roleName);
    Task<bool> CurrentUserHasRole(RoleEnum role);
    Task<bool> CurrentUserHasRoleAnyAsync(Func<RoleEnum?, bool> predicate);
    Task<bool> HasRole(Guid userId, string roleName);
    Task<bool> HasRole(Guid userId, RoleEnum role);
    Task<bool> SendEmailConfirmationAsync(Guid userId);
    Task<IEnumerable<UserCompany>?> CurrentUserCompaniesAsync();
    Task<IEnumerable<UserCompanyModel>?> CurrentUserCompaniesWithNameAsync();
    Task<IEnumerable<Guid>> CurrentUserCompanyIdsAsync();
    Task<IEnumerable<UserCompany>?> UserCompaniesAsync(Guid id);
    Task<IEnumerable<UserCompanyModel>?> UserCompaniesWithNameAsync(Guid id);
    Task<bool> CurrentUserHasCompanyAsync(Guid companyId);
    Task<bool> UserHasCompanyAsync(User user, Guid companyId);
}