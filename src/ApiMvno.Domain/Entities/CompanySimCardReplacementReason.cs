using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class CompanySimCardReplacementReason : Entity
    {

        public Guid CompanyId { get; set; }
        public Guid SimCardReplacementReasonId { get; set; }

        #region Relationships

        public virtual Company Company { get; set; }
        public virtual SimCardReplacementReason SimCardReplacementReason { get; set; }

        #endregion
    }
}
