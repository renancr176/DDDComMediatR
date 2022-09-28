using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities;

public class CompanyDueDay : Entity
{
    public Guid CompanyId { get; set; }
    public Guid DueDayId { get; set; }

    #region Relationships

    public virtual Company Company { get; set; }
    public virtual DueDay DueDay { get; set; }
    public virtual ICollection<CompanyAccountType> CompanyAccountTypes { get; set; }

    #endregion
}