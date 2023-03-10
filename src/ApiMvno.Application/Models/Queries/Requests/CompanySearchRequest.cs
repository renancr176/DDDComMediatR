namespace ApiMvno.Application.Models.Queries.Requests;

public class CompanySearchRequest : PagedRequest
{
    public string? Email { get; set; }
    public string? Document { get; set; }
    public string? Name { get; set; }
}