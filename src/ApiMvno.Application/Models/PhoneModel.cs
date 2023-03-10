namespace ApiMvno.Application.Models;

public class PhoneModel : EntityModel
{
    public long PhoneTypeId { get; set; }
    public long Number { get; set; }
    public PhoneTypeModel PhoneType { get; set; }
}