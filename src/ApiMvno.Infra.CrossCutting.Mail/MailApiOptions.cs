namespace ApiMvno.Infra.CrossCutting.Mail;

public class MailApiOptions
{
    public static string sectionKey = "MailApi";
    public string Url { get; set; }
    public string Token { get; set; }

    public bool Ok()
    {
        if (!string.IsNullOrEmpty(Url)
            && Uri.IsWellFormedUriString(Url, UriKind.Absolute)
            && !string.IsNullOrEmpty(Token))
            return true;
        return false;
    }
}