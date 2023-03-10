using ApiMvno.Application.Commands.PhoneCommands;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class UpdateCompanyPhoneCommand : Command<CompanyPhoneModel?> 
{
    public Guid Id { get; set; }
    public bool Active { get; set; }
    public UpdatePhoneCommand Phone { get; set; }

    public UpdateCompanyPhoneCommand()
    {
    }

    public UpdateCompanyPhoneCommand(Guid id, bool active, UpdatePhoneCommand phone)
    {
        Id = id;
        Active = active;
        Phone = phone;
    }
}