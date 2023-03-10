using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Interfaces;

public interface IBackServiceCustomerService
{
    Task<BackServiceBaseResponse<CreateCustomerResponse>> CreateAsync(CreateCustomerRequest request);
}
