namespace ApiMvno.Infra.CrossCutting.Portability;

public class PortabilityOptions
{
    public static string sectionKey = "Portability";

    public string Url { get; set; }
    public string UserName{ get; set; }
    public string Password { get; set; }

    public bool ok()
    {
        if (Uri.IsWellFormedUriString(Url, UriKind.Absolute)
            && !string.IsNullOrEmpty(UserName)
            && !string.IsNullOrEmpty(Password))
            return true;
        
        return false;
    }
}