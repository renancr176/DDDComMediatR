using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class CompanyAddress : Entity
    {
        public Guid CompanyId { get; set; }
        public Guid AddressId { get; set; }

        #region Relationships

        public virtual Company Company { get; set; }
        public virtual Address Address { get; set; }

        #endregion

        public CompanyAddress()
        {
        }

        public CompanyAddress(Guid addressId)
        {
            AddressId = addressId;
        }

        public CompanyAddress(Address address)
        {
            Address = address;
        }
    }
}
