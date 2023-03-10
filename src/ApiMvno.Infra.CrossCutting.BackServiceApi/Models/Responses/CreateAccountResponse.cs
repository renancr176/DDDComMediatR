namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;

public class CreateAccountResponse : BaseModel
{
    public string Name { get; set; }

    public string CustomerExternalCode { get; set; }

    public string CustomerName { get; set; }
}
