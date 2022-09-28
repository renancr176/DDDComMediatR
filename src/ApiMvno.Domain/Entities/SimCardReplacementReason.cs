using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class SimCardReplacementReason : Entity
    {
        public string Description { get; set; }
        public bool Active { get; set; } = true;

        #region Relationships

        public virtual ICollection<CompanySimCardReplacementReason> CompanySimCardReplacementReasons { get; set; }

        #endregion
    }
}
