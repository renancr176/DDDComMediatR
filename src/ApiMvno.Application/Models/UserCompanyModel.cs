namespace ApiMvno.Application.Models;

public class UserCompanyModel
{
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? CreateByUserId { get; set; }
    public Guid? DeletedByUserId { get; set; }
    public string? CompanyName { get; set; }
}