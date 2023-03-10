using System.Net;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Interfaces;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;
using ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;
using Flurl.Http;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Services;

public class BackServiceCompanyService : IBackServiceCompanyService
{
    private readonly IBackServiceAuthService _backServiceAuthService;

    public IFlurlRequest Url => _backServiceAuthService.Url
        .AppendPathSegment("/Company");

    public BackServiceCompanyService(IBackServiceAuthService backServiceAuthService)
    {
        _backServiceAuthService = backServiceAuthService;
    }

    public async Task<BackServiceBaseResponse<CompanyModel>> CreateAsync(CreateCompanyRequest request)
    {
        var response = new BackServiceBaseResponse<CompanyModel>();

        try
        {
            var result = await Url.PostJsonAsync(request);
            response = await result.GetJsonAsync<BackServiceBaseResponse<CompanyModel>>();
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

    public async Task<BackServiceBaseResponse<CompanyModel>> UpdateAsync(UpdateCompanyRequest request)
    {
        var response = new BackServiceBaseResponse<CompanyModel>();

        try
        {
            var result = await Url.PutJsonAsync(request);
            response = await result.GetJsonAsync<BackServiceBaseResponse<CompanyModel>>();
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