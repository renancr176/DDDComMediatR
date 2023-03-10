using ApiMvno.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ApiMvno.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; }
    public UserStatusEnum Status { get; set; } = UserStatusEnum.Active;

    #region Relationships

    public virtual ICollection<UserCompany> UserCompanies { get; set; }
    public virtual ICollection<UserCompany> UserCreatedCompanies { get; set; }
    public virtual ICollection<UserCompany> UserDeletedCompanies { get; set; }

    #endregion

    public User()
    {
    }

    public User(string userName, string name, UserStatusEnum status)
        : base(userName)
    {
        Name = name;
        Email = userName;
        Status = status;
    }
}