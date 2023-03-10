using ApiMvno.Application.Commands.AddressCommands;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class UpdateCompanyAddressCommand : Command<CompanyAddressModel?>
{
    public Guid Id { get; set; }
    public UpdateAddressCommand Address { get; set; }

    public UpdateCompanyAddressCommand()
    {       
    }

    public UpdateCompanyAddressCommand(Guid id, UpdateAddressCommand address)
    {
        Id = id;
        Address = address;
    }
}