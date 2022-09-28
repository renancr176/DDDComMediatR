namespace ApiMvno.Infra.CrossCutting.Sms;

public class SmsApiAuthToken
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}