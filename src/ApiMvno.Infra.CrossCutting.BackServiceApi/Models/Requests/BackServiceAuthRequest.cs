namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;

public class BackServiceAuthRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public BackServiceAuthRequest(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}