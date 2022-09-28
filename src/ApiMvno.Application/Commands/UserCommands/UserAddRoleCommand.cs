using System.ComponentModel.DataAnnotations;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.UserCommands;

public class UserAddRoleCommand : Command
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public IEnumerable<string> Roles { get; set; }

    public UserAddRoleCommand()
    {
    }

    public UserAddRoleCommand(Guid userId, IEnumerable<string> roles)
    {
        UserId = userId;
        Roles = roles;
    }
}