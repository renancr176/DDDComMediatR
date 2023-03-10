using System.Net;
using ApiMvno.Infra.CrossCutting.Jsc.Interfaces;
using ApiMvno.Infra.CrossCutting.Jsc.Models;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ApiMvno.Infra.CrossCutting.Jsc.Services;

public class JscMsisdnService : IJscMsisdnService
{
    private readonly IOptions<JscOptions> _jscOptions;
    private readonly IJscAuthService _jscAuthService;

    public JscMsisdnService(IOptions<JscOptions> jscOptions, IJscAuthService jscAuthService)
    {
        _jscOptions = jscOptions;
        _jscAuthService = jscAuthService;
    }

    public JscOptions JscOptions => _jscOptions.Value;

    public IFlurlRequest Url => JscOptions.UrlApiBss
        .AppendPathSegment("/v1/msisdn");

    public async Task<JscPagedResponse<JscMsisdnModel>> SearchAsync(MsisdnSearchRequest request)
    {
        var response = new JscPagedResponse<JscMsisdnModel>();

        try
        {
            var authResult = await _jscAuthService.GetAuthAsync(request);
            if (!authResult.Success)
            {
                response.Errors = authResult.Errors;
            }
            else
            {
                var result = await Url
                    .WithOAuthBearerToken(authResult.Data.AccessToken)
                    .SendJsonAsync(HttpMethod.Get, request);

                response = await result.GetJsonAsync<JscPagedResponse<JscMsisdnModel>>();
            }
        }
        catch (FlurlHttpException e)
        {
            response.Errors.Add(
                new JscBaseResponseError()
                {
                    Code = ((HttpStatusCode)e.Call.HttpResponseMessage.StatusCode).ToString(),
                    Message = await e.Call.HttpResponseMessage.Content.ReadAsStringAsync(),
                }
            );
        }
        catch (Exception e)
        {
            response.Errors.Add(
                new JscBaseResponseError()
                {
                    Code = "Exception",
                    Message = e.Message,
                }
            );
        }

        return response;
    }
}