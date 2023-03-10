
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Interfaces;

public interface IBackServiceAccountService
{
    Task<BackServiceBaseResponse<CreateAccountResponse>> CreateAsync(CreateAccountRequest request);
}
