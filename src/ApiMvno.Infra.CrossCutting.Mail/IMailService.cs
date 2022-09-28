namespace ApiMvno.Infra.CrossCutting.Mail;

public interface IMailService
{
    MailApiOptions Config { get; }
    Task<bool> SendAsync(SendMailResquest sendMailResquest);
}