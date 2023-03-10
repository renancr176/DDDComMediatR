using System.ComponentModel.DataAnnotations;
using ApiMvno.Application.Models;
using ApiMvno.Domain.Core.Enums;
using ApiMvno.Domain.Core.Messages;

namespace ApiMvno.Application.Commands.UserCommands;

public class SignUpCommand : Command<UserModel>
{
    [Required(ErrorMessage = "User name is required.")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "User name must have from 6 to 50 characters.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Name must have from 6 to 50 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%¨&*_+-=^~?<>]).{8,50}$", ErrorMessage = "Password pattern not match.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public IEnumerable<RoleEnum> Roles { get; set; }
}