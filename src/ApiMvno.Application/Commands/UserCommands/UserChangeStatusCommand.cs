using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiMvno.Domain.Core.Messages;
using ApiMvno.Domain.Enums;

namespace ApiMvno.Application.Commands.UserCommands;

public class UserChangeStatusCommand : Command
{
    [Required(ErrorMessage = "Error_UserIdIsRequired")]
    public Guid UserId { get; set; }
    [Required(ErrorMessage = "Error_StatusIsRequired")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserStatusEnum Status { get; set; }
}