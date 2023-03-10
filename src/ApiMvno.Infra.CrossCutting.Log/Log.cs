using System.Text.Json;
using ApiMvno.Infra.CrossCutting.ElasticSearch;
using ApiMvno.Infra.CrossCutting.MongoDB;
using ApiMvno.Infra.CrossCutting.MongoDB.Collections;
using Microsoft.Extensions.Logging;

namespace ApiMvno.Infra.CrossCutting.Log;

public class Log : ILog
{
    private readonly ILogger<Log> _logger;
    private readonly IElasticSearchClient _elasticSearchClient;
    private readonly IMongoDBClient _mongoDBClient;

    public Log(ILogger<Log> logger, IElasticSearchClient elasticSearchClient, IMongoDBClient mongoDbClient)
    {
        _logger = logger;
        _elasticSearchClient = elasticSearchClient;
        _mongoDBClient = mongoDbClient;
    }

    public async Task LogAsync(LogLevelEnum logLevel, object data, string? traceIdentifier = null, string? index = null, CancellationToken cancellationToken = default)
    {
        try
        {
            await _elasticSearchClient.SendEventAsync(
                new
                {
                    TraceIdentifier = traceIdentifier,
                    Level = logLevel,
                    Data = data
                },
                index,
                cancellationToken
            );

            await _mongoDBClient.SendEventAsync(
                new LogCollection()
                {
                    Data = new
                    {
                        TraceIdentifier = traceIdentifier,
                        Level = logLevel,
                        Data = data
                    }
                },
                cancellationToken
            );
        }
        catch (Exception e)
        {
            _logger.LogError(1, e, e.Message);
            switch (logLevel)
            {
                case LogLevelEnum.Error:
                    _logger.LogError(1, JsonSerializer.Serialize(data));
                    break;
                case LogLevelEnum.Warning:
                    _logger.LogWarning(1, JsonSerializer.Serialize(data));
                    break;
                default:
                    _logger.LogInformation(1, JsonSerializer.Serialize(data));
                    break;
            }
        }
    }

    public async Task LogAsync(Exception exception, string? traceIdentifier = null, string? index = null, CancellationToken cancellationToken = default)
    {
        try
        {
            await _elasticSearchClient.SendEventAsync(
                new
                {
                    TraceIdentifier = traceIdentifier,
                    Level = LogLevelEnum.Error,
                    Exception = exception
                },
                index,
                cancellationToken
            );

            await _mongoDBClient.SendEventAsync(
                new LogCollection()
                {
                    Data = new {
                        TraceIdentifier = traceIdentifier,
                        Level = LogLevelEnum.Error,
                        Exception = exception
                    }
                },
                cancellationToken
            );
        }
        catch (Exception e)
        {
            _logger.LogError(1, e, e.Message);
            _logger.LogError(1, exception, exception.Message);
        }
    }
}