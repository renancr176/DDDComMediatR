using ApiMvno.Infra.CrossCutting.BackServiceApi.Models;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Interfaces;

public interface IBackServiceCompanyService
{
    Task<BackServiceBaseResponse<CompanyModel>> CreateAsync(CreateCompanyRequest request);
    Task<BackServiceBaseResponse<CompanyModel>> UpdateAsync(UpdateCompanyRequest request);
}