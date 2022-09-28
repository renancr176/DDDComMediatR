using ApiMvno.Application.Commands.AddressCommands;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.CompanyCommands
{
    public class CreateCompanyAddressesCommand : Command<CompanyAddressModel>
    {
        public CreateAddressCommand Address { get; set; }
    }
}
