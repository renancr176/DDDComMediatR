using ApiMvno.Infra.CrossCutting.BackServiceApi.Enums;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;

public class CreateCustomerResponse : BaseModel
{
    public CustomerTypeEnum CustomerType { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public string LandLinePhone { get; set; }
    public string MobilePhone { get; set; }
    public string Name { get; set; }
    public DateTime Birthdate { get; set; }
    public string Nationality { get; set; }
}
