namespace ApiMvno.Application.Models;

public class SingInResponseModel
{
    public string? AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserModel User { get; set; }
}