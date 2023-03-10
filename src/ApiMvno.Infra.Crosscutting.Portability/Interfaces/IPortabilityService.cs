using ApiMvno.Infra.CrossCutting.Portability.Models.Requests;
using ApiMvno.Infra.CrossCutting.Portability.Models.Responses;

namespace ApiMvno.Infra.CrossCutting.Portability.Interfaces;

public interface IPortabilityService
{
    Task<PortabilityBaseResponse<PortabilityApplyResponse>> ApplyPortabilityAsync(PortabilityApplyRequest request);
    Task<PortabilityBaseResponse<PortabilityCancelResponse>> CancelPortabilityAsync(PortabilityCancelRequest request);
}