namespace ApiMvno.Infra.CrossCutting.Sms;

public class SmsApiAuthResponse
{
    public SmsApiAuthToken Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}