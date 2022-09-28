namespace ApiMvno.Infra.CrossCutting.Sms;

public interface ISmsService
{
    Task<bool> SendAsync(SendSmsRequest request);
}