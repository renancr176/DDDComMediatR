using System.Text.Json.Serialization;
using ApiMvno.Domain.Enums;

namespace ApiMvno.Application.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserStatusEnum Status { get; set; }
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}