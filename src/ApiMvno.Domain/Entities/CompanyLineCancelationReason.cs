using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities;

public class CompanyLineCancelationReason : Entity
{
    public Guid CompanyId { get; set; }
    public long LineCancelationReasonId { get; set; }

    #region Relationships

    public virtual Company Company { get; set; }
    public virtual LineCancelationReason LineCancelationReason { get; set; }

    #endregion
}