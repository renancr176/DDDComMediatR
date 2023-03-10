using ApiMvno.Infra.CrossCutting.BackServiceApi.Enums;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;

public class CreateSalesforceAccountAddressRequest
{
    public SalesforceAccountAddressTypeEnum Type { get; set; }
    public string ZipCode { get; set; }
    public string StreetName { get; set; }
    public double StreetNumber { get; set; }
    public string StreetComplement { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    
    public CreateSalesforceAccountAddressRequest(SalesforceAccountAddressTypeEnum type, string zipCode,
        string streetName, double streetNumber, string streetComplement, string neighborhood, string city, string state,
        string country)
    {
        Type = type;
        ZipCode = zipCode;
        StreetName = streetName;
        StreetNumber = streetNumber;
        StreetComplement = streetComplement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
    }
}
