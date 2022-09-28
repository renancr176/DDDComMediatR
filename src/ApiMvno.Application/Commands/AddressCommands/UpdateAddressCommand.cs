using ApiMvno.Domain.Enums;

namespace ApiMvno.Application.Commands.AddressCommands
{
    public class UpdateAddressCommand
    {

        public UpdateAddressCommand(Guid id,
                                    AddressTypeEnum addressType,
                                    string zipCode,
                                    string state,
                                    string city,
                                    string neighborhood,
                                    string streetName,
                                    string streetNumber,
                                    string details,
                                    Guid countryId)
        {
            Id = id;
            AddressType = addressType;
            ZipCode = zipCode;
            State = state;
            City = city;
            Neighborhood = neighborhood;
            StreetName = streetName;
            StreetNumber = streetNumber;
            Details = details;
            CountryId = countryId;
        }

        public Guid Id { get; private set; }
        public AddressTypeEnum AddressType { get; private set; } = AddressTypeEnum.Shipping;
        public string ZipCode { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string Neighborhood { get; private set; }
        public string StreetName { get; private set; }
        public string StreetNumber { get; private set; }
        public string Details { get; private set; }
        public Guid CountryId { get; private set; }

    }
}
