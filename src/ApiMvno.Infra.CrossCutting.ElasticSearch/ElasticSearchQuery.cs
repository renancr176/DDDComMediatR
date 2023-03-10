using System.Diagnostics.CodeAnalysis;

namespace ApiMvno.Infra.CrossCutting.ElasticSearch;

[ExcludeFromCodeCoverage]
public class ElasticSearchQuery
{
    public TimeSpan Scroll { get; set; }

    public int SuggestionSize { get; set; }
}