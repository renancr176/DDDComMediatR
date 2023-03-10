using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.CountryCommands;

public class CreateCountryCommand : Command<CountryModel?>
{
    public string Name { get; set; }
    public string PhoneCode { get; set; }
    public bool Active { get; set; }

    public CreateCountryCommand()
    {
    }

    public CreateCountryCommand(string name, string phoneCode, bool active)
    {
        Name = name;
        PhoneCode = phoneCode;
        Active = active;
    }
}