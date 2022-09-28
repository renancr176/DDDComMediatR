namespace ApiMvno.Infra.CrossCutting.Sms;

public class SmsApiAuthRequest
{
    public string Email { get; set; }
    public string Password { get; set; }

    public SmsApiAuthRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
}