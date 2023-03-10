using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ApiMvno.Infra.CrossCutting.HubDev;

public class HubDevService : IHubDevService
{
    private readonly IOptions<HubDevOptions> _options;

    public HubDevOptions HubDevOptions => _options.Value;

    public Url Url => HubDevOptions.Url.SetQueryParam("token", HubDevOptions.Token);

    public HubDevService(IOptions<HubDevOptions> options)
    {
        _options = options;
    }

    public async Task<CpfResponse?> GetCPF(string cpf, string data)
    {
        try
        {
            return await Url.AppendPathSegment("/cpf")
                .SetQueryParams(new
                {
                    cpf,
                    data
                })
                .GetJsonAsync<CpfResponse>();
        }
        catch (FlurlHttpException){}
        catch (Exception){}

        return null;
    }

    public async Task<CnpjResponse?> GetCNPJ(string cnpj)
    {
        try
        {
            return await HubDevOptions.Url.AppendPathSegment("/cnpj")
                .SetQueryParam(cnpj)
                .GetJsonAsync<CnpjResponse>();
        }
        catch (FlurlHttpException) { }
        catch (Exception) { }

        return null;
    }

    public async Task<CepResponse?> GetCEP(string cep)
    {
        try
        {
            return await HubDevOptions.Url.AppendPathSegment("/cep3")
                .SetQueryParam(cep)
                .GetJsonAsync<CepResponse>();
        }
        catch (FlurlHttpException) { }
        catch (Exception) { }

        return null;
    }
}