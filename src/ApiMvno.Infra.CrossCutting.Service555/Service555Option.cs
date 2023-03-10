using System.Net;

namespace ApiMvno.Infra.CrossCutting.Service555;

public class Service555Option
{
    public static string sectionKey = "Service555";

    public bool Active { get; set; }
    public string? Ip { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }

    public bool Ok()
    {
        if (!string.IsNullOrEmpty(Ip?.Trim())
        && IPAddress.TryParse(Ip, out var teste)
        && !string.IsNullOrEmpty(UserName?.Trim())
        && !string.IsNullOrEmpty(Password?.Trim()))
        {
            return true;
        }
        return false;
    }

}