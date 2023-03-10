using System.Net;
using ApiMvno.Infra.CrossCutting.Jsc.Extensions;
using ApiMvno.Infra.CrossCutting.Jsc.Interfaces;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ApiMvno.Infra.CrossCutting.Jsc.Services;

public class JscAuthService : IJscAuthService
{
    private readonly IOptions<JscOptions> _jscOptions;

    public JscOptions JscOptions => _jscOptions.Value;

    public IFlurlRequest Url => JscOptions.UrlApiBss
        .AppendPathSegment("/v1/auth/login");

    public Dictionary<string, AuthResponse> AuthResponses { get; private set; } = new Dictionary<string, AuthResponse>();

    public JscAuthService(IOptions<JscOptions> jscOptions)
    {
        _jscOptions = jscOptions;
    }

    public async Task<JscBaseResponse<AuthResponse>> GetAuthAsync(JscAuthRequest request)
    {
        var response = new JscBaseResponse<AuthResponse>();

        var authKey = $"{request.UserName}.{request.Password}.{request.BrandId}".GetHashString();
        AuthResponse authResponse;

        if (AuthResponses.TryGetValue(authKey, out authResponse)
            && authResponse != null
            && authResponse.ExpiresIn > DateTime.UtcNow)
        {
            response.Data = authResponse;
            return response;
        }

        try
        {
            var result = await Url
                .SetQueryParams(new
                {
                    user = request.UserName,
                    pass = request.Password,
                    brandID = request.BrandId
                })
                .PutJsonAsync(null);

            response.Data = await result.GetJsonAsync<AuthResponse>();

            if (AuthResponses.TryGetValue(authKey, out authResponse))
            {
                authResponse = response.Data;
            }
            else
            {
                AuthResponses.Add(authKey, response.Data);
            }
        }
        catch (FlurlHttpException e)
        {
            response.Errors.Add(
                new JscBaseResponseError()
                {
                    Code = ((HttpStatusCode)e.Call.HttpResponseMessage.StatusCode).ToString(),
                    Message = await e.Call.HttpResponseMessage.Content.ReadAsStringAsync()
                }
            );
        }
        catch (Exception e)
        {
            response.Errors.Add(
                new JscBaseResponseError()
                {
                    Code = "Exception",
                    Message = e.Message
                }
            );
        }

        return response;
    }
}