using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ApiMvno.Test.Extensions;

public static class HttpClientExtensions
{
    public static HttpClient AddToken(this HttpClient client, string token)
    {
        client = client.AddJsonMediaType();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    public static HttpClient AddJsonMediaType(this HttpClient client)
    {
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //client.AddLanguage();
        return client;
    }

    //public static HttpClient AddLanguage(this HttpClient client, LanguageEnum language = LanguageEnum.Portugues)
    //{
    //    client.DefaultRequestHeaders.AcceptLanguage.Clear();
    //    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(language.GetDescription()));
    //    return client;
    //}

    public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this HttpClient client, string? requestUri,
        TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        JsonContent content = JsonContent.Create(value, mediaType: null, options);
        var request = new HttpRequestMessage(HttpMethod.Patch, requestUri)
        {
            Content = content
        };

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return client.SendAsync(request, cancellationToken);
    }
}