namespace ApiMvno.Application.Models
{
    public class AddressModel : EntityModel
    {
        public long AddressTypeId { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string Details { get; set; }
        public Guid CountryId { get; set; }

        public CountryModel Country { get; set; }
        public AddressTypeModel AddressType { get; set; }
    }
}
