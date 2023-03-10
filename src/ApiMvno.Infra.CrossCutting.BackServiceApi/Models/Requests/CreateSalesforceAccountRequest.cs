namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;

public class CreateSalesforceAccountRequest : BackServiceBaseRequest
{
    public Guid MvnoCustomerId{ get; set; }
    public string? SalesforceCode { get; set; }
    public string Name { get; set; }
    public string Nationality { get; set; }
    public bool National { get; set; }
    public string Document { get; set; }
    public string MvnoCode { get; set; }
    public decimal MvnoId { get; set; }
    public string Phone { get; set; }
    public string Cellphone { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public string PersonType { get; set; }
    public string FantasyName { get; set; }
    public string InscMun { get; set; }
    public string InscEstad { get; set; }
    public IEnumerable<CreateSalesforceAccountAddressRequest> SalesforceAccountAddresses { get; set; }

    public CreateSalesforceAccountRequest(string requestId, Guid mvnoCustomerId, string? salesforceCode, string name,
        string nationality, bool national, string document, string mvnoCode, decimal mvnoId, string phone,
        string cellphone, string email, string status, string personType, string fantasyName, string inscMun,
        string inscEstad, IEnumerable<CreateSalesforceAccountAddressRequest> salesforceAccountAddresses) :
        base(requestId)
    {
        MvnoCustomerId = mvnoCustomerId;
        SalesforceCode = salesforceCode;
        Name = name;
        Nationality = nationality;
        National = national;
        Document = document;
        MvnoCode = mvnoCode;
        MvnoId = mvnoId;
        Phone = phone;
        Cellphone = cellphone;
        Email = email;
        Status = status;
        PersonType = personType;
        FantasyName = fantasyName;
        InscMun = inscMun;
        InscEstad = inscEstad;
        SalesforceAccountAddresses = salesforceAccountAddresses;
    }
}
