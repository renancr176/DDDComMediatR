using ApiMvno.Infra.CrossCutting.Jsc.Models;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;

namespace ApiMvno.Infra.CrossCutting.Jsc.Interfaces;

public interface IJscSubscriptionService
{
    Task<JscBaseResponse> UpdateStatusAsync(UpdateSubscriptionStatusRequest request);
    Task<JscPagedResponse<JscFeatureModel>> GetFeaturesAsync(SubscriptionFeaturesGetRequest request);
    Task<JscBaseResponse> PatchFeaturesAsync(SubscriptionFeaturesPatchRequest request);
}