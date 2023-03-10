using ApiMvno.Infra.CrossCutting.BackServiceApi.Interfaces;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;
using Flurl.Http;
using System.Net;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Services;

public class BackServiceSalesforceService : IBackServiceSalesforceService
{
    private readonly IBackServiceAuthService _backServiceAuthService;


    public IFlurlRequest Url => _backServiceAuthService.Url
        .AppendPathSegment("/SalesforceAccount");

    public BackServiceSalesforceService(IBackServiceAuthService backServiceAuthService)
    {
        _backServiceAuthService = backServiceAuthService;
    }

    public async Task<BackServiceBaseResponse<SalesforceAccountResponse>> CreateAsync(CreateSalesforceAccountRequest request)
    {
        var response = new BackServiceBaseResponse<SalesforceAccountResponse>();

        try
        {
            var result = await Url.PostJsonAsync(request);
            response = await result.GetJsonAsync<BackServiceBaseResponse<SalesforceAccountResponse>>();
        }
        catch (FlurlHttpException e)
        {
            response.Errors.Add(
               new BackServiceBaseResponseError()
               {
                   ErrorCode = ((HttpStatusCode)e.Call.HttpResponseMessage.StatusCode).ToString(),
                   Message = await e.Call.HttpResponseMessage.Content.ReadAsStringAsync(),
               }
           );
        }
        catch (Exception e)
        {
            response.Errors.Add(
                new BackServiceBaseResponseError()
                {
                    ErrorCode = "Exception",
                    Message = e.Message,
                }
            );
        }
        return response;
    }
}