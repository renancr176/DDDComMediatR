using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;
using ApiMvno.Domain.Enums;

namespace ApiMvno.Application.Commands.AddressCommands
{
    public class CreateAddressCommand : Command<AddressModel>
    {
        
        public Guid Id { get; private set; }
        public Guid CountryId { get; private set; }
        public AddressTypeEnum AddressType { get; private set; }
        public string ZipCode { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string Neighborhood { get; private set; }
        public string StreetName { get; private set; }
        public string StreetNumber { get; private set; }
        public string Details { get; private set; }
    }
}
