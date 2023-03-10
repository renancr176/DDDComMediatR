namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;

public class AuthResponse
{
    public string AccessToken { get; set; }
    public string TokenType { get; set; }
    public DateTime ExpiresIn { get; set; }
}