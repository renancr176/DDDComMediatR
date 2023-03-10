
namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;

public class CreateAccountRequest : BackServiceBaseRequest
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Name { get; set; }
    public string CustomerExternalCode { get; set; }
    public string CustomerName { get; set; }

    public CreateAccountRequest(string requestId, Guid id, Guid companyId, string name, string customerExternalCode,
        string customerName) 
        : base(requestId)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        CustomerExternalCode = customerExternalCode;
        CustomerName = customerName;
    }
}
