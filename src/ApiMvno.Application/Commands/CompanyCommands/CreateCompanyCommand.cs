using ApiMvno.Application.Commands.AddressCommands;
using ApiMvno.Application.Commands.PhoneCommands;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class CreateCompanyCommand : Command<CompanyModel?>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }
    public IEnumerable<CreateAddressCommand> Addresses { get; set; }
    public IEnumerable<CreatePhoneCommand> Phones { get; set; }

    public CreateCompanyCommand()
    {
    }

    public CreateCompanyCommand(string name, string email, string document, IEnumerable<CreateAddressCommand> addresses,
        IEnumerable<CreatePhoneCommand> phones)
    {
        Name = name;
        Email = email;
        Document = document;
        Addresses = addresses;
        Phones = phones;
    }
}