using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class Address : Entity
    {
        public Guid CountryId { get; set; }
        public long AddressTypeId { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string Details { get; set; }

        #region Relationships

        public virtual Country Country { get; set; }
        public virtual AddressType AddressType { get; set; }
        public virtual ICollection<CompanyAddress> CompanyAddresses { get; set; }

        #endregion
    }
}
