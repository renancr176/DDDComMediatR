using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class CompanyCn : Entity
    {
        public Guid CompanyId { get; set; }
        public Guid CnId { get; set; }

        #region Relationships

        public virtual Company Company { get; set; }
        public virtual Cn Cns { get; set; }

        #endregion
    }
}
