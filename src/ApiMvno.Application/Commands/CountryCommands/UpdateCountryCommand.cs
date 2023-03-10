using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.CountryCommands;

public class UpdateCountryCommand : Command<CountryModel?>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneCode { get; set; }
    public bool Active { get; set; }

    public UpdateCountryCommand()
    {
    }

    public UpdateCountryCommand(Guid id, string name, string phoneCode, bool active)
    {
        Id = id;
        Name = name;
        PhoneCode = phoneCode;
        Active = active;
    }
}