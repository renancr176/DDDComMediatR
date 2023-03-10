using ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;
using ApiMvno.Infra.CrossCutting.Jsc.Models;

namespace ApiMvno.Infra.CrossCutting.Jsc.Interfaces;

public interface IJscSimCardService
{
    Task<JscBaseResponse<JscSimCardModel>> GetAsync(JscSimCardRequest request);
}
