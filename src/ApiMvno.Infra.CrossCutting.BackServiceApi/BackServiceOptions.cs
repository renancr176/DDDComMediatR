namespace ApiMvno.Infra.CrossCutting.BackServiceApi;

public class BackServiceOptions
{
    public static string sectionKey = "BackService";

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