using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.UserCommands;

public class UserAddCompanyCommand : Command
{
    public Guid UserId { get; set; }
    public IEnumerable<Guid> CompaniesId { get; set; }
}