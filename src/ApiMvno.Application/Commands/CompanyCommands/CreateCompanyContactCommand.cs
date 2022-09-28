using ApiMvno.Application.Commands.PhoneCommands;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class CreateCompanyContactCommand
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Position { get; set; }
    public bool NotifyChanges { get; set; }
    public IEnumerable<CreatePhoneCommand> Phones { get; set; }
}