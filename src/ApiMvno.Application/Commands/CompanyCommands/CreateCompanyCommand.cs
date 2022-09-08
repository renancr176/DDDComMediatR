using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.CompanyCommands;

public class CreateCompanyCommand : Command<CompanyModel>
{
    public string Name { get; set; }
}