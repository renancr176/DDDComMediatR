namespace ApiMvno.Infra.CrossCutting.Sms;

public class SmsApiOptions
{
    public static string sectionKey = "SmsApi";

    public string Url { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Enabled { get; set; }

    public bool Ok()
    {
        if (!string.IsNullOrEmpty(Url)
            && Uri.IsWellFormedUriString(Url, UriKind.Absolute)
            && !string.IsNullOrEmpty(Email)
            && !string.IsNullOrEmpty(Password))
            return true;
        return false;
    }
}