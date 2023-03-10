using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.ElasticSearch;

public static class ElasticSearchIoC
{
    public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ElasticSearchOptions>(configuration.GetSection(ElasticSearchOptions.sectionKey));
        services.AddSingleton<IElasticSearchClient, ElasticSearchClient>();
    }
}