using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;

namespace ApiMvno.Infra.CrossCutting.ElasticSearch;

public class ElasticSearchClient : IElasticSearchClient
{
    private readonly IOptions<ElasticSearchOptions> _elasticSearchOptions;
    private readonly ILogger<ElasticSearchClient> _logger;

    public ElasticSearchOptions Config => _elasticSearchOptions.Value;

    /// <summary>Initializes a new instance of the <see cref="ElasticSearchClient" /> class.</summary>
    /// <param name="elasticSearchOptions">The elastic search options.</param>
    public ElasticSearchClient(IOptions<ElasticSearchOptions> elasticSearchOptions,
                               ILogger<ElasticSearchClient> logger)
    {
        _elasticSearchOptions = elasticSearchOptions;
        _logger = logger;
    }

    private async Task<ElasticClient> GetClient<TClass>(string? index = null)
        where TClass : class
    {
        index = index ?? Config.Index;

        if (string.IsNullOrEmpty(Config.Url)) throw new ArgumentException(nameof(ElasticSearchOptions.Url));
        if (string.IsNullOrEmpty(index)) throw new ArgumentException(nameof(ElasticSearchOptions.Index));

        var node = new Uri(Config.Url);
        index = string.Format(index, DateTime.Now);

        var settings = new ConnectionSettings(node)
          .DefaultFieldNameInferrer(f => f.ToLowerInvariant())
          .DefaultIndex(index)
          .ThrowExceptions()
          .PrettyJson()
          .RequestTimeout(TimeSpan.FromSeconds(30));

        switch (Config.AuthenticationType)
        {
            case ElasticSearchAuthenticationEnum.Token:
                settings = settings.ApiKeyAuthentication(new ApiKeyAuthenticationCredentials(Config.Token));
                break;
            case ElasticSearchAuthenticationEnum.Basic:
                settings = settings.BasicAuthentication(Config.User, Config.Password);
                break;
            default:
                break;
        }

        var client = new ElasticClient(settings);

        if (!client.Indices.Exists(index).Exists)
        {
            try
            {
                var asyncCreateIndexResponse = await client.Indices
                    .CreateAsync(index, c => c.Map<TClass>(m => m
                        .AutoMap()));

                if (!asyncCreateIndexResponse.IsValid)
                    _logger.LogCritical($@"Error on create new index ""{index}"" into elastic search, see datails: \n{asyncCreateIndexResponse.DebugInformation}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $@"Error on create new index ""{index}"" into elastic search");
            }
        }

        return client;
    }

    /// <summary>Sends the event asynchronous.</summary>
    /// <param name="event">The event.</param>
    /// <param name="channel">The channel.</param>
    /// <param name="ct">The ct.</param>
    /// <exception cref="System.ArgumentException">channel</exception>
    /// <exception cref="Telecall.Framework.Exceptions.IntegrationException"></exception>
    public async Task SendEventAsync<TClass>(TClass @event,
                                     string? index = null,
                                     CancellationToken cancellationToken = default)
        where TClass : class
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!Config.Enabled) return;

        var client = await GetClient<TClass>(index);

        var asyncIndexResponse = await client.IndexDocumentAsync(@event, cancellationToken);

        if (!asyncIndexResponse.IsValid)
            _logger.LogCritical($"Erro ao enviar dados para o elastic search Detalhes: {asyncIndexResponse.DebugInformation}");
    }
}