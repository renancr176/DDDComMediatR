namespace ApiMvno.Infra.CrossCutting.Sms;

public class SendSmsRequest
{
    public string Destination { get; set; }
    public string Message { get; set; }

    public SendSmsRequest(string destination, string message)
    {
        Destination = destination;
        Message = message;
    }
}