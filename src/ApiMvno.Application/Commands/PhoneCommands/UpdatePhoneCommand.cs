using ApiMvno.Domain.Core.Messages;
using ApiMvno.Domain.Entities;

namespace ApiMvno.Application.Commands.PhoneCommands;

public class UpdatePhoneCommand : Command<Phone?>
{
    public Guid Id { get; set; }
    public long PhoneTypeId { get; set; }
    public long Number { get; set; }

    public UpdatePhoneCommand()
    {
    }

    public UpdatePhoneCommand(Guid id, long phoneTypeId, long number)
    {
        Id = id;
        PhoneTypeId = phoneTypeId;
        Number = number;
    }
}