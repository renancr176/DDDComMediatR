namespace ApiMvno.Application.Models;

public class CountryModel : EntityModel
{
    public string Name { get; set; }
    public string PhoneCode { get; set; }
    public bool Active { get; set; } = true;
}