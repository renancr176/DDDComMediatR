using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiMvno.Infra.CrossCutting.Mail;

public class SendMailResquest
{
    [Required(ErrorMessage = "Errors_EmailIsRequired")]
    [EmailAddress(ErrorMessage = "Errors_InvalidEmail")]
    [JsonProperty("EmailDestino")]
    public string EmailDestination { get; set; }
    [Required(ErrorMessage = "Errors_SubjectIsRequired")]
    [JsonProperty("Assunto")]
    public string Subject { get; set; }
    public bool IsHtml { get; set; } = true;
    public string Template { get; set; } = "default";
    [Required(ErrorMessage = "Errors_MessageIsRequired")]
    [JsonProperty("BodyMessage")]
    public string Body { get; set; }

    public SendMailResquest(string emailDestination, string subject, string body)
    {
        EmailDestination = emailDestination;
        Subject = subject;
        Body = body;
    }

    public SendMailResquest(string emailDestination, string subject, string body, bool isHtml, string template)
        : this(emailDestination, subject, body)
    {
        IsHtml = isHtml;
        Template = template;
    }
}