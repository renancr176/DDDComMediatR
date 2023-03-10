namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;

public class SalesforceAccountResponse
{
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
    
    public IEnumerable<SalesforceAccountAddressResponse> SalesforceAccountAddresses { get; set; }
}