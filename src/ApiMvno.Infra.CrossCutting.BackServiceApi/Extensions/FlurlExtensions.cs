using Flurl.Http;
using Newtonsoft.Json;
using NullValueHandling = Flurl.NullValueHandling;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Extensions;

public static class FlurlExtensions
{
    public static IFlurlRequest SetJsonQueryParams(this IFlurlRequest request, object values,
        NullValueHandling nullValueHandling = NullValueHandling.Remove)
    {
        var valueSerialized = JsonConvert.SerializeObject(values);
        values = JsonConvert.DeserializeObject<Dictionary<string, string>>(valueSerialized);
        return request.SetQueryParams(values);
    }
}