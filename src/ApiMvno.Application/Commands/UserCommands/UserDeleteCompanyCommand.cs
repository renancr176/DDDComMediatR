using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.UserCommands;

public class UserDeleteCompanyCommand : Command
{
    public Guid Id { get; set; }
}