using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;
using Flurl.Http;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Interfaces;

public interface IBackServiceAuthService
{
    public BackServiceOptions BackServiceOptions { get; }
    public BackServiceAuthResponse Auth { get; }
    public IFlurlRequest Url { get; }
}