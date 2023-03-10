namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;

public class UpdateCompanyRequest : CreateCompanyRequest
{
    public UpdateCompanyRequest(string requestId, Guid id, string externalCode, string externalUserName,
        string externalPassword) 
        : base(requestId, id, externalCode, externalUserName, externalPassword)
    {
    }
}