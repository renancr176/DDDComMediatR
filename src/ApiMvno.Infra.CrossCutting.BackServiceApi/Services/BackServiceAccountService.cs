using ApiMvno.Infra.CrossCutting.BackServiceApi.Interfaces;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;
using Flurl.Http;
using System.Net;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Services;

public class BackServiceAccountService : IBackServiceAccountService
{
    private readonly IBackServiceAuthService _backServiceAuthService;

    public IFlurlRequest Url => _backServiceAuthService.Url
      .AppendPathSegment("/Account");

    public BackServiceAccountService(IBackServiceAuthService backServiceAuthService)
    {
        _backServiceAuthService = backServiceAuthService;
    }

    public async Task<BackServiceBaseResponse<CreateAccountResponse>> CreateAsync(CreateAccountRequest request)
    {
        var response = new BackServiceBaseResponse<CreateAccountResponse>();

        try
        {
            var result = await Url.PostJsonAsync(request);
            response = await result.GetJsonAsync<BackServiceBaseResponse<CreateAccountResponse>>();
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
