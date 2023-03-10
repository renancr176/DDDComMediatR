
using ApiMvno.Infra.CrossCutting.BackServiceApi.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public UserStatusEnum Status { get; set; }
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}