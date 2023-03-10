using Newtonsoft.Json;

namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;

public class JscAuthRequest
{
    [JsonIgnore]
    public string UserName { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    [JsonIgnore]
    public string BrandId { get; set; }

    public JscAuthRequest(string userName, string password, string brandId)
    {
        UserName = userName;
        Password = password;
        BrandId = brandId;
    }
}