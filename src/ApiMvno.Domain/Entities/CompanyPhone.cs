using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class CompanyPhone : Entity
    {
        public Guid CompanyId { get; set; }
        public Guid PhoneId { get; set; }

        #region Relationships

        public virtual Company Company { get; set; }
        public virtual Phone Phone { get; set; }
        
        #endregion
    }
}
