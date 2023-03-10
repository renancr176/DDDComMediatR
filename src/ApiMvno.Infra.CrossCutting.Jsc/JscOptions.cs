using Flurl.Http;
using Flurl;
using Flurl.Http.Configuration;
using Newtonsoft.Json;

namespace ApiMvno.Infra.CrossCutting.Jsc;

public class JscOptions
{
    public static string sectionKey = "Jsc";

    public string? Domain { get; set; }
    public string? ApiBssPath { get; set; }
    public string? ApiFlowsPath { get; set; }
    public string DomainApiBss => $"{Domain}/{ApiBssPath}";
    public string DomainApiFlows => $"{Domain}/{ApiFlowsPath}";

    public void Check()
    {
        if (string.IsNullOrEmpty(Domain)
            || !Uri.IsWellFormedUriString(Domain, UriKind.Absolute))
        {
            throw new ArgumentNullException(nameof(Domain), $"The {nameof(Domain)} URI is null or is not well formed.");
        }

        if (string.IsNullOrEmpty(ApiBssPath)
            || !Uri.IsWellFormedUriString(DomainApiBss, UriKind.Absolute))
        {
            throw new ArgumentNullException(nameof(ApiBssPath), $"The {nameof(ApiBssPath)} is null or is invalid.");
        }

        if (string.IsNullOrEmpty(ApiFlowsPath)
            || !Uri.IsWellFormedUriString(DomainApiFlows, UriKind.Absolute))
        {
            throw new ArgumentNullException(nameof(ApiFlowsPath), $"The {nameof(ApiFlowsPath)} is null or is invalid.");
        }
    }

    public IFlurlRequest UrlApiBss => new Url(DomainApiBss)
        .Clone()
        .ConfigureRequest(settings =>
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };
            settings.JsonSerializer = new NewtonsoftJsonSerializer(jsonSettings);
        });

    public IFlurlRequest UrlApiFlows => new Url(DomainApiFlows)
        .Clone()
        .ConfigureRequest(settings =>
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };
            settings.JsonSerializer = new NewtonsoftJsonSerializer(jsonSettings);
        });
}