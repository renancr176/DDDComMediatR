using ApiMvno.Domain.Core.DomainObjects;
using ApiMvno.Domain.Enums;

namespace ApiMvno.Domain.Entities;

public class AccountType : EntityIntId
{
    public AccountTypeEnum Type { get; set; }
    public string Name { get; set; }
    public bool RequireDueDay { get; set; }
    public bool Active { get; set; } = true;

    #region Relationships

    public virtual ICollection<CompanyAccountType> CompanyAccountTypes { get; set; }

    #endregion
}