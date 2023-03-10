using ApiMvno.Infra.CrossCutting.Jsc.Models;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;

namespace ApiMvno.Infra.CrossCutting.Jsc.Interfaces;

public interface IJscConsumptionService
{
    Task<JscPagedResponse<JscConsumptionDetailModel>> GetDetails(ConsumptionDetailsRequest request);
    Task<JscBaseResponse<ConsumptionTotalsResponse>> GetTotals(ConsumptionTotalsRequest request);
}