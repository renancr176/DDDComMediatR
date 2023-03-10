namespace ApiMvno.Infra.CrossCutting.RabbitMQ;

public class RabbitMqOptions
{
    public static string sectionKey = "RabbitMQ";

    public string? HostName { get; set; } = "http://localhost";
    public int Port { get; set; } = 5672;
    public string? UserName { get; set; } = "guest";
    public string? Password { get; set; } = "guest";
    public bool Enabled { get; set; }

    public bool Ok()
    {
        if (!string.IsNullOrEmpty(HostName)
            && Uri.IsWellFormedUriString(HostName, UriKind.Absolute)
            && Port > 0
            && !string.IsNullOrEmpty(UserName)
            && !string.IsNullOrEmpty(Password))
        {
            return true;
        }

        return false;
    }
}