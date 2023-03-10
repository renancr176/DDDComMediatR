namespace ApiMvno.Application.Models;

public class CompanyModel : EntityModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }
    public IEnumerable<CompanyAddressModel> CompanyAddresses { get; set; }
    public IEnumerable<CompanyPhoneModel> CompanyPhones { get; set; }
}