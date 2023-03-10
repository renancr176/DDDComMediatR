namespace ApiMvno.Infra.CrossCutting.Portability.Models.Responses;

public class PortabilityAuthResponse
{
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsValid()
    {
        if (!string.IsNullOrEmpty(AccessToken)
            && CreatedAt.Add(TimeSpan.FromSeconds(ExpiresIn)) > DateTime.UtcNow)
            return true;
        return false;
    }
}