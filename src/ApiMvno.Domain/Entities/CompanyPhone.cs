using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class CompanyPhone : Entity
    {
        public Guid CompanyId { get; set; }
        public Guid PhoneId { get; set; }
        public bool Active { get; set; } = true;

        #region Relationships

        public virtual Company Company { get; set; }
        public virtual Phone Phone { get; set; }
        
        #endregion

        public CompanyPhone()
        {
        }

        public CompanyPhone(Guid phoneId)
        {
            PhoneId = phoneId;
        }

        public CompanyPhone(Phone phone)
        {
            Phone = phone;
        }
    }
}
