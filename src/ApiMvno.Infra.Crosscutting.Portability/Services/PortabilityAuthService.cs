using System.Net;
using ApiMvno.Infra.CrossCutting.Portability.Interfaces;
using ApiMvno.Infra.CrossCutting.Portability.Models.Requests;
using ApiMvno.Infra.CrossCutting.Portability.Models.Responses;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ApiMvno.Infra.CrossCutting.Portability.Services;

public class PortabilityAuthService : IPortabilityAuthService
{
    public PortabilityBaseResponse<PortabilityAuthResponse> PortabilityAuthResponse { get; private set; }
    
    private readonly IOptions<PortabilityOptions> _backServiceOptions;

    public PortabilityOptions PortabilityOptions => _backServiceOptions.Value;

    private static PortabilityAuthResponse _auth;
    public PortabilityAuthResponse Auth => GetAuth();

    public IFlurlRequest Url => new Url(PortabilityOptions.Url)
        .Clone()
        .WithOAuthBearerToken(Auth.AccessToken)
        .AllowHttpStatus(HttpStatusCode.BadRequest);
    
    #region Consts

    public const string PortabilityOptionsError = "Portability options is not properly cofigured.";

    #endregion

    public PortabilityAuthService(IOptions<PortabilityOptions> backServiceOptions)
    {
        _backServiceOptions = backServiceOptions;
    }

    private PortabilityAuthResponse GetAuth()
    {
        if (PortabilityAuthResponse?.Data is not null
            && PortabilityAuthResponse.Data.IsValid())
        {
            return _auth;
        }

        if (PortabilityOptions is null || !PortabilityOptions.ok())
        {
            throw new ArgumentNullException(nameof(PortabilityOptionsError), PortabilityOptionsError);
        }

        Task.Run(async () =>
            {
                var result = await new Url(PortabilityOptions.Url).AppendPathSegment("/Auth/SignIn").PostJsonAsync(new
                    PortabilityAuthRequest(PortabilityOptions.UserName, PortabilityOptions.Password));
                
                var response = await result.GetJsonAsync<PortabilityBaseResponse<PortabilityAuthResponse>>();
                if (response.Success)
                {
                    _auth = response.Data;
                }
            }
        ).Wait();
        
        return _auth;
    }
}