namespace ApiMvno.Infra.CrossCutting.Portability.Models.Requests;

public class PortabilityAuthRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public PortabilityAuthRequest(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}