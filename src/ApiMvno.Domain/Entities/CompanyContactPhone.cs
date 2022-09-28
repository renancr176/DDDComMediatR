using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class CompanyContactPhone : Entity
    {
        public Guid CompanyContactId { get; set; }
        public Guid PhoneId { get; set; }

        #region Relationships

        public virtual CompanyContact CompanyContact { get; set; }
        public virtual Phone Phone { get; set; }

        #endregion
    }
}
