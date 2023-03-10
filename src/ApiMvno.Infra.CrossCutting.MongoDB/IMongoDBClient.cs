using ApiMvno.Infra.CrossCutting.MongoDB.Collections;

namespace ApiMvno.Infra.CrossCutting.MongoDB;

public interface IMongoDBClient
{
    MongoDBOptions Config { get; }
    Task SendEventAsync(LogCollection body, CancellationToken cancellationToken = default);
}