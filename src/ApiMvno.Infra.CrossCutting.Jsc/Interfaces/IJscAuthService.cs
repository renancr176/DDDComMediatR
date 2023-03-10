using ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;

namespace ApiMvno.Infra.CrossCutting.Jsc.Interfaces;

public interface IJscAuthService
{
    Task<JscBaseResponse<AuthResponse>> GetAuthAsync(JscAuthRequest request);
}