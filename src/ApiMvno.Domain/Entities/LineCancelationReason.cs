using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities;

public class LineCancelationReason : EntityIntId
{
    public string Description { get; set; }
    public bool Active { get; set; } = true;

    #region Relationships

    public virtual ICollection<CompanyLineCancelationReason> CompanyLineCancelationReasons { get; set; }

    #endregion
}