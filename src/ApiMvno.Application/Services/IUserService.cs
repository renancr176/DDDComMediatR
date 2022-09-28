using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ApiMvno.Application.Services;

public interface IUserService
{
    Task<User> CurrentUserAsync();
    Task<User> FindByUserName(string userName);
    Task<bool> CurrentUserHasRole(string roleName);
    Task<bool> CurrentUserHasRole(RoleEnum role);
    Task<bool> HasRole(Guid userId, string roleName);
    Task<bool> HasRole(Guid userId, RoleEnum role);
    Task<bool> SendEmailConfirmationAsync(Guid userId);
}