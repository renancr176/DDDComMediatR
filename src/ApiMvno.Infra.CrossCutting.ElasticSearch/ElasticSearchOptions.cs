using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ApiMvno.Infra.CrossCutting.ElasticSearch;

[ExcludeFromCodeCoverage]
public class ElasticSearchOptions
{
    public static string sectionKey = "ElasticSearch";

    public string? Index { get; set; } = $"{Assembly.GetExecutingAssembly().GetName().Name}-{{0:yyyy.MM}}";

    public string? Url { get; set; } = string.Empty;

    public string? User { get; set; }

    public string? Password { get; set; }

    public string? Token { get; set; }

    public TimeSpan Timeout { get; set; }

    public ElasticSearchQuery? Query { get; set; }

    public bool Enabled { get; set; }

    public ElasticSearchAuthenticationEnum AuthenticationType { get; set; }
}