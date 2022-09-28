using System.ComponentModel.DataAnnotations;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.UserCommands;

public class PasswordResetCommand : Command<MessageModel>
{
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string UserName { get; set; }
}