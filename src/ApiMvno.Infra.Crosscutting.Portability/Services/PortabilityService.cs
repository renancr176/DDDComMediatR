using ApiMvno.Infra.CrossCutting.Portability.Interfaces;
using ApiMvno.Infra.CrossCutting.Portability.Models.Requests;
using ApiMvno.Infra.CrossCutting.Portability.Models.Responses;
using Flurl.Http;
using System.Net;

namespace ApiMvno.Infra.CrossCutting.Portability.Services;

public class PortabilityService : IPortabilityService
{
    private readonly IPortabilityAuthService _backServiceAuthService;


    public IFlurlRequest Url => _backServiceAuthService.Url
        .AppendPathSegment("/Portability");

    public PortabilityService(IPortabilityAuthService backServiceAuthService)
    {
        _backServiceAuthService = backServiceAuthService;
    }

    public async Task<PortabilityBaseResponse<PortabilityApplyResponse>> ApplyPortabilityAsync(PortabilityApplyRequest request)
    {
        var response = new PortabilityBaseResponse<PortabilityApplyResponse>();

        try
        {
            var result = await Url.AppendPathSegment("/applyportability").PostJsonAsync(request);
            response = await result.GetJsonAsync<PortabilityBaseResponse<PortabilityApplyResponse>>();
        }
        catch (FlurlHttpException e)
        {
            response.Errors.Add(
                new PortabilityBaseResponseError()
                {
                    ErrorCode = ((HttpStatusCode)e.Call.HttpResponseMessage.StatusCode).ToString(),
                    Message = await e.Call.HttpResponseMessage.Content.ReadAsStringAsync(),
                }
            );
        }
        catch (Exception e)
        {
            response.Errors.Add(
                new PortabilityBaseResponseError()
                {
                    ErrorCode = "Exception",
                    Message = e.Message,
                }
            );
        }
        return response;
    }

    public async Task<PortabilityBaseResponse<PortabilityCancelResponse>> CancelPortabilityAsync(PortabilityCancelRequest request)
    {
        var response = new PortabilityBaseResponse<PortabilityCancelResponse>();

        try
        {
            var result = await Url.AppendPathSegment("cancelportability").PostJsonAsync(request);
            response = await result.GetJsonAsync<PortabilityBaseResponse<PortabilityCancelResponse>>();
        }
        catch (FlurlHttpException e)
        {
            response.Errors.Add(
                new PortabilityBaseResponseError()
                {
                    ErrorCode = ((HttpStatusCode)e.Call.HttpResponseMessage.StatusCode).ToString(),
                    Message = await e.Call.HttpResponseMessage.Content.ReadAsStringAsync(),
                }
            );
        }
        catch (Exception e)
        {
            response.Errors.Add(
                new PortabilityBaseResponseError()
                {
                    ErrorCode = "Exception",
                    Message = e.Message,
                }
            );
        }
        return response;
    }
}