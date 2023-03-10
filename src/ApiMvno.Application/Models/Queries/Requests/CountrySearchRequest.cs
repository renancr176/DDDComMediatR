namespace ApiMvno.Application.Models.Queries.Requests;

public class CountrySearchRequest : PagedRequest
{
    public string? Name { get; set; }
    public string? PhoneCode { get; set; }
}