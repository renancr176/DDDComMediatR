using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.PhoneCommands;

public class CreatePhoneCommand : Command<PhoneModel?>
{
    public long PhoneTypeId { get; set; }
    public long Number { get; set; }

    public CreatePhoneCommand()
    {
    }

    public CreatePhoneCommand(long phoneTypeId, long number)
    {
        PhoneTypeId = phoneTypeId;
        Number = number;
    }
}