using ApiMvno.Infra.CrossCutting.BackServiceApi.Interfaces;
using Flurl.Http;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;
using System.Net;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Services;

public class BackServiceCustomerService : IBackServiceCustomerService
{
    private readonly IBackServiceAuthService _backServiceAuthService;

    public IFlurlRequest Url => _backServiceAuthService.Url
      .AppendPathSegment("/Customer");

    public BackServiceCustomerService(IBackServiceAuthService backServiceAuthService)
    {
        _backServiceAuthService = backServiceAuthService;
    }

    public async Task<BackServiceBaseResponse<CreateCustomerResponse>> CreateAsync(CreateCustomerRequest request)
    {
        var response = new BackServiceBaseResponse<CreateCustomerResponse>();

        try
        {
            var result = await Url.PostJsonAsync(request);
            response = await result.GetJsonAsync<BackServiceBaseResponse<CreateCustomerResponse>>();
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
