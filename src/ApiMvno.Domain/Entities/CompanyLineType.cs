using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public  class CompanyLineType : Entity
    {
        public Guid CompanyId { get; set; }
        public Guid LineTypeId { get; set; }
        public Guid TaxProductId { get; set; }
        public bool Active { get; set; }

        #region Relationships

        public virtual LineType LineType { get; set; }
        public virtual Company Company { get; set; }

        #endregion
    }
}
