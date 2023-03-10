using System.Net;
using ApiMvno.Infra.CrossCutting.Jsc.Extensions;
using ApiMvno.Infra.CrossCutting.Jsc.Interfaces;
using ApiMvno.Infra.CrossCutting.Jsc.Models;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ApiMvno.Infra.CrossCutting.Jsc.Services;

public class JscPackageService : IJscPackageService
{
    private readonly IOptions<JscOptions> _jscOptions;
    private readonly IJscAuthService _jscAuthService;

    public JscOptions JscOptions => _jscOptions.Value;

    public IFlurlRequest Url => JscOptions.UrlApiBss
        .AppendPathSegment("/v1/package");

    public JscPackageService(IOptions<JscOptions> jscOptions, IJscAuthService jscAuthService)
    {
        _jscOptions = jscOptions;
        _jscAuthService = jscAuthService;
    }

    public async Task<JscPagedResponse<JscPackageModel>> SearchAsync(PackageSearchRequest request)
    {
        var response = new JscPagedResponse<JscPackageModel>();

        try
        {
            var authResult = await _jscAuthService.GetAuthAsync(request);
            if (!authResult.Success)
            {
                response.Errors = authResult.Errors;
            }
            else
            {
                response = await Url
                    .WithOAuthBearerToken(authResult.Data.AccessToken)
                    .SetJsonQueryParams(request)
                    .GetJsonAsync<JscPagedResponse<JscPackageModel>>();
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