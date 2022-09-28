using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities;

public class CompanyAccountType : Entity
{
    public Guid CompanyId { get; set; }
    public long AccountTypeId { get; set; }
    public Guid? CompanyDueDayId { get; set; }
    public bool Active { get; set; }

    #region Relationships

    public virtual Company Company { get; set; }
    public virtual AccountType AccountType { get; set; }
    public virtual CompanyDueDay CompanyDueDay { get; set; }

    #endregion
}