namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;

public class CreateCompanyRequest : BackServiceBaseRequest
{
    public Guid Id { get; set; }
    public string ExternalCode { get; set; }
    public string ExternalUserName { get; set; }
    public string ExternalPassword { get; set; }

    public CreateCompanyRequest(string requestId, Guid id, string externalCode, string externalUserName,
        string externalPassword) 
        : base(requestId)
    {
        Id = id;
        ExternalCode = externalCode;
        ExternalUserName = externalUserName;
        ExternalPassword = externalPassword;
    }
}
