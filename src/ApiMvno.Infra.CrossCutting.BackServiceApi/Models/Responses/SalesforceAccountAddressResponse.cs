using ApiMvno.Infra.CrossCutting.BackServiceApi.Enums;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;

public class SalesforceAccountAddressResponse
{
    public Guid SalesforceAccountId { get; set; }
    public SalesforceAccountAddressTypeEnum Type { get; set; }
    public string ZipCode { get; set; }
    public string StreetName { get; set; }
    public double StreetNumber { get; set; }
    public string StreetComplement { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}