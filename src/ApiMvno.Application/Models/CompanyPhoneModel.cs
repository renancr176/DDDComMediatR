namespace ApiMvno.Application.Models;

public class CompanyPhoneModel :EntityModel
{
    public Guid CompanyId { get; set; }
    public Guid PhoneId { get; set; }

    public  PhoneModel Phone { get; set; }

}