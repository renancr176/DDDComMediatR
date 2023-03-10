using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.AddressCommands
{
    public class CreateAddressCommand : Command<AddressModel>
    {
        public long AddressTypeId { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string? Details { get; set; }
        public Guid CountryId { get; set; }

        public CreateAddressCommand()
        {
        }

        public CreateAddressCommand(long addressTypeId, string zipCode, string state,
            string city, string neighborhood, string streetName, int streetNumber,
            string? details, Guid countryId)
        {
            AddressTypeId = addressTypeId;
            ZipCode = zipCode;
            State = state;
            City = city;
            Neighborhood = neighborhood;
            StreetName = streetName;
            StreetNumber = streetNumber;
            Details = details;
            CountryId = countryId;
        }
    }
}
