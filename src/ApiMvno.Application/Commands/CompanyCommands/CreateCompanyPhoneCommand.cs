using ApiMvno.Application.Commands.PhoneCommands;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class CreateCompanyPhoneCommand : Command<CompanyPhoneModel?>
{
    public Guid CompanyId { get; set; }
    public CreatePhoneCommand Phone { get; set; }
}