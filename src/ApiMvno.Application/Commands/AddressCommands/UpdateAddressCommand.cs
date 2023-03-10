using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.AddressCommands
{
    public class UpdateAddressCommand : Command<AddressModel?>
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public long AddressTypeId { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string? Details { get; set; }
        
        public UpdateAddressCommand()
        {
        }

        public UpdateAddressCommand(Guid id, Guid countryId, long addressTypeId, string zipCode, string state,
            string city, string neighborhood, string streetName, int streetNumber, string? details)
        {
            Id = id;
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
    }
}
