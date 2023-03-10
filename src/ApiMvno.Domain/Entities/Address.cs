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
        public string? Details { get; set; }

        #region Relationships

        public virtual Country Country { get; set; }
        public virtual AddressType AddressType { get; set; }
        public virtual ICollection<CompanyAddress> CompanyAddresses { get; set; }

        #endregion

        public Address()
        {
        }

        public Address(Guid countryId, long addressTypeId, string zipCode, string state, string city,
            string neighborhood, string streetName, int streetNumber, string? details = null)
        {
            CountryId = countryId;
            AddressTypeId = addressTypeId;
            ZipCode = zipCode;
            State = state;
            City = city;
            Neighborhood = neighborhood;
            StreetName = streetName;
            StreetNumber = streetNumber;
            Details = details;
        }

        public Address(Guid id, Guid countryId, long addressTypeId, string zipCode, string state, string city,
           string neighborhood, string streetName, int streetNumber, string? details = null)
            : this(countryId, addressTypeId, zipCode, state, city, neighborhood, streetName, streetNumber, details)
        {
            Id = id != Guid.Empty ? id : Guid.NewGuid();
        }
    }
}
