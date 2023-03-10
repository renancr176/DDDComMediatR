using ApiMvno.Infra.CrossCutting.Portability.Models.Responses;
using Flurl.Http;

namespace ApiMvno.Infra.CrossCutting.Portability.Interfaces;

public interface IPortabilityAuthService
{
    public PortabilityOptions PortabilityOptions { get; }
    public PortabilityAuthResponse Auth { get; }
    public IFlurlRequest Url { get; }
}