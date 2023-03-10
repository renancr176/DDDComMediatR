using ApiMvno.Application.Models;
using ApiMvno.Application.Models.Queries.Requests;
using ApiMvno.Application.Models.Queries.Responses;

namespace ApiMvno.Application.Queries.Interfaces;

public interface ICompanyQuery
{
    Task<CompanyModel?> GetByIdAsync(Guid id);
    Task<PagedResponse<CompanyModel>?> SearchAsync(CompanySearchRequest request);
    Task<IEnumerable<CompanyPhoneModel>?> GetAllPhonesByCompanyIdAsync(Guid companyId);
    Task<IEnumerable<CompanyAddressModel>?> GetAllAddressesByCompanyIdAsync(Guid companyId);
}