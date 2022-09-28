using ApiMvno.Domain.Enums;

namespace ApiMvno.Application.Models
{
    public class AddressModel : EntityModel
    {
        public AddressTypeEnum AddressType { get; set; } = AddressTypeEnum.Shipping;
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Details { get; set; }
        public Guid CountryId { get; set; }
    }
}
