using System.Net;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Interfaces;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Services;

public class BackServiceAuthService : IBackServiceAuthService
{
    public BackServiceBaseResponse<BackServiceAuthResponse> BackServiceAuthResponse { get; private set; }
    
    private readonly IOptions<BackServiceOptions> _backServiceOptions;

    public BackServiceOptions BackServiceOptions => _backServiceOptions.Value;

    
    public BackServiceAuthResponse Auth => GetAuth();

    public IFlurlRequest Url => new Url(BackServiceOptions.Url)
        .Clone()
        .WithOAuthBearerToken(Auth.AccessToken)
        .AllowHttpStatus(HttpStatusCode.BadRequest);
    
    #region Consts

    public const string BackServiceOptionsError = "Back service options is not properly cofigured.";

    #endregion

    public BackServiceAuthService(IOptions<BackServiceOptions> backServiceOptions)
    {
        _backServiceOptions = backServiceOptions;
    }

    private BackServiceAuthResponse GetAuth()
    {
        if (BackServiceAuthResponse?.Data is not null
            && BackServiceAuthResponse.Data.IsValid())
        {
            return BackServiceAuthResponse.Data;
        }

        if (BackServiceOptions is null || !BackServiceOptions.ok())
        {
            throw new ArgumentNullException(nameof(BackServiceOptionsError), BackServiceOptionsError);
        }

        Task.Run(async () =>
            {
                var result = await new Url(BackServiceOptions.Url).AppendPathSegment("/User/SignIn").PostJsonAsync(new
                    BackServiceAuthRequest(BackServiceOptions.UserName, BackServiceOptions.Password));
                
                BackServiceAuthResponse = await result.GetJsonAsync<BackServiceBaseResponse<BackServiceAuthResponse>>();
            }
        ).Wait();
        
        return BackServiceAuthResponse?.Data;
    }
}