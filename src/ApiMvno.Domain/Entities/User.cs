using ApiMvno.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ApiMvno.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; }
    public UserStatusEnum Status { get; set; } = UserStatusEnum.Active;
}