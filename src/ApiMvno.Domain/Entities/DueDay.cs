using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities;

public class DueDay : Entity
{
    public int Day { get; set; }
    public int CycleStart { get; set; }
    public int CycleEnd { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; } = true;

    #region RelationShips

    public virtual ICollection<CompanyDueDay> CompanyDueDays { get; set; }

    #endregion
}