using ApiMvno.Application.Commands.AddressCommands;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.CompanyCommands
{
    public class CreateCompanyAddressCommand : Command<CompanyAddressModel?>
    {
        public Guid CompanyId { get; set; }
        public CreateAddressCommand Address { get; set; }
    }
}
