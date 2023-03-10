using ApiMvno.Application.Models;
using ApiMvno.Application.Models.Queries.Requests;
using ApiMvno.Application.Models.Queries.Responses;

namespace ApiMvno.Application.Queries.Interfaces;

public interface ICountryQuery
{
    Task<CountryModel?> GetByIdAsync(Guid id);
    Task<IEnumerable<CountryModel>?> GetAllAsync();
    Task<PagedResponse<CountryModel>?> SearchAsync(CountrySearchRequest request);
}