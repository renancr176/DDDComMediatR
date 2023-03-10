using ApiMvno.Infra.CrossCutting.Jsc.Interfaces;
using Flurl.Http;
using Flurl;
using Microsoft.Extensions.Options;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;
using ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;
using System.Net;
using ApiMvno.Infra.CrossCutting.Jsc.Models;

namespace ApiMvno.Infra.CrossCutting.Jsc.Services;

public class JscSimCardService : IJscSimCardService
{
    private readonly IOptions<JscOptions> _jscOptions;
    private readonly IJscAuthService _jscAuthService;

    public JscOptions JscOptions => _jscOptions.Value;

    public IFlurlRequest Url => JscOptions.UrlApiBss
        .AppendPathSegment("/v1/sim");

    public JscSimCardService(IOptions<JscOptions> jscOptions, IJscAuthService jscAuthService)
    {
        _jscOptions = jscOptions;
        _jscAuthService = jscAuthService;
    }

    public async Task<JscBaseResponse<JscSimCardModel>> GetAsync(JscSimCardRequest request)
    {
        var response = new JscBaseResponse<JscSimCardModel>();

        try
        {
            var authResult = await _jscAuthService.GetAuthAsync(request);
            if (!authResult.Success)
            {
                response.Errors = authResult.Errors;
            }
            else
            {
                response.Data = await Url
                    .AppendPathSegment($"/{request.Iccid}")
                    .WithOAuthBearerToken(authResult.Data.AccessToken)
                    .GetJsonAsync<JscSimCardModel>();
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
