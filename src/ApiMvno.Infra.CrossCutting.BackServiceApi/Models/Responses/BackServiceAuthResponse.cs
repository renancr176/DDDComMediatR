namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;

public class BackServiceAuthResponse
{
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public UserModel User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsValid()
    {
        if (!string.IsNullOrEmpty(AccessToken)
            && CreatedAt.Add(TimeSpan.FromSeconds(ExpiresIn)) > DateTime.UtcNow)
            return true;
        return false;
    }
}