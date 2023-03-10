using ApiMvno.Infra.CrossCutting.Jsc.Interfaces;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System.Net;
using ApiMvno.Infra.CrossCutting.Jsc.Models;

namespace ApiMvno.Infra.CrossCutting.Jsc.Services;

public class JscSubscriptionService : IJscSubscriptionService
{
    private readonly IOptions<JscOptions> _jscOptions;
    private readonly IJscAuthService _jscAuthService;

    public JscSubscriptionService(IOptions<JscOptions> jscOptions, IJscAuthService jscAuthService)
    {
        _jscOptions = jscOptions;
        _jscAuthService = jscAuthService;
    }

    public JscOptions JscOptions => _jscOptions.Value;

    public IFlurlRequest Url => JscOptions.UrlApiBss
        .AppendPathSegment("/v1/subscription");

    public async Task<JscBaseResponse> UpdateStatusAsync(UpdateSubscriptionStatusRequest request)
    {
        var response = new JscBaseResponse();

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
                    .AppendPathSegment($"/{request.SubscriptionId}")
                    .SetQueryParam("action", "CHANGE_STATUS")
                    .WithOAuthBearerToken(authResult.Data.AccessToken)
                    .PatchJsonAsync(new { request.Status });

                response = await result.GetJsonAsync<JscBaseResponse>();
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

    public async Task<JscPagedResponse<JscFeatureModel>> GetFeaturesAsync(
        SubscriptionFeaturesGetRequest request)
    {
        var response = new JscPagedResponse<JscFeatureModel>();

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
                    .AppendPathSegment($"/{request.SubscriptionId}/features")
                    .WithOAuthBearerToken(authResult.Data.AccessToken)
                    .SendJsonAsync(HttpMethod.Get, request);

                response = await result.GetJsonAsync<JscPagedResponse<JscFeatureModel>>();
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

    public async Task<JscBaseResponse> PatchFeaturesAsync(SubscriptionFeaturesPatchRequest request)
    {
        var response = new JscBaseResponse();

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
                    .AppendPathSegment($"/{request.SubscriptionId}/features")
                    .WithOAuthBearerToken(authResult.Data.AccessToken)
                    .PatchJsonAsync(request.Features);

                response = await result.GetJsonAsync<JscBaseResponse>() ?? new JscBaseResponse();
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