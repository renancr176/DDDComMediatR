namespace ApiMvno.Infra.CrossCutting.HubDev;

public class HubDevOptions
{
    public static string sectionKey = "HubDev";

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