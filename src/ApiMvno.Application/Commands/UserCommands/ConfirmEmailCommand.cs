using System.ComponentModel.DataAnnotations;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.UserCommands;

public class ConfirmEmailCommand : Command
{
    [Required]
    public string Token { get; set; }
}