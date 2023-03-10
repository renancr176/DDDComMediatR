using ApiMvno.Infra.CrossCutting.Jsc.Interfaces;
using ApiMvno.Infra.CrossCutting.Jsc.Models;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System.Net;

namespace ApiMvno.Infra.CrossCutting.Jsc.Services;

public class JscConsumptionService : IJscConsumptionService
{
    private readonly IOptions<JscOptions> _jscOptions;
    private readonly IJscAuthService _jscAuthService;

    public JscConsumptionService(IOptions<JscOptions> jscOptions, IJscAuthService jscAuthService)
    {
        _jscOptions = jscOptions;
        _jscAuthService = jscAuthService;
    }

    public JscOptions JscOptions => _jscOptions.Value;

    public IFlurlRequest Url => JscOptions.UrlApiBss
        .AppendPathSegment("/v1/consumption");

    public async Task<JscPagedResponse<JscConsumptionDetailModel>> GetDetails(ConsumptionDetailsRequest request)
    {
        var response = new JscPagedResponse<JscConsumptionDetailModel>();

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
                    .AppendPathSegment($"/{request.SubscriptionId}/details")
                    .WithOAuthBearerToken(authResult.Data.AccessToken)
                    .SendJsonAsync(HttpMethod.Get, request);

                response = await result.GetJsonAsync<JscPagedResponse<JscConsumptionDetailModel>>();
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

    public async Task<JscBaseResponse<ConsumptionTotalsResponse>> GetTotals(ConsumptionTotalsRequest request)
    {
        var response = new JscBaseResponse<ConsumptionTotalsResponse>();

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
                    .AppendPathSegment($"/{request.SubscriptionId}/totals")
                    .WithOAuthBearerToken(authResult.Data.AccessToken)
                    .SendJsonAsync(HttpMethod.Get, request);

                response = await result.GetJsonAsync<JscBaseResponse<ConsumptionTotalsResponse>>();
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