using ApiMvno.Infra.CrossCutting.Jsc.Models;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;

namespace ApiMvno.Infra.CrossCutting.Jsc.Interfaces;

public interface IJscMsisdnService
{
    Task<JscPagedResponse<JscMsisdnModel>> SearchAsync(MsisdnSearchRequest request);
}