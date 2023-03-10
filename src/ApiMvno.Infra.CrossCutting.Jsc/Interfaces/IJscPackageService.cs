using ApiMvno.Infra.CrossCutting.Jsc.Models;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;

namespace ApiMvno.Infra.CrossCutting.Jsc.Interfaces;

public interface IJscPackageService
{
    Task<JscPagedResponse<JscPackageModel>> SearchAsync(PackageSearchRequest request);
}