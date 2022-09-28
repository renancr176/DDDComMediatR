using System.ComponentModel.DataAnnotations;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.UserCommands;

public class SingInCommand : Command<SingInResponseModel>
{
    [Required(ErrorMessage = "User name is required.")]
    [EmailAddress(ErrorMessage = "Invalid user name.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Error_PasswordRequired")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%¨&*_+-=^~?<>]).{8,50}$", ErrorMessage = "Errors_PasswordPatternNotMatch")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}