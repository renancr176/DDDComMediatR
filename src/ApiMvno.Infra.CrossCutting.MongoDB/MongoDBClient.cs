using ApiMvno.Infra.CrossCutting.MongoDB.Collections;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace ApiMvno.Infra.CrossCutting.MongoDB;

public class MongoDBClient : IMongoDBClient
{
    private readonly IOptions<MongoDBOptions> _mongoDBOptions;
    
    private IMongoCollection<BsonDocument> _logCollection;
    public MongoDBOptions Config => _mongoDBOptions.Value;
    
    public MongoDBClient(IOptions<MongoDBOptions> mongoDbOptions)
    {
        _mongoDBOptions = mongoDbOptions;
    }

    private async Task<IMongoCollection<BsonDocument>> GetClient()
    {
        if (string.IsNullOrEmpty(Config.ConnectionString)) throw new ArgumentException(nameof(MongoDBOptions.ConnectionString));
        if (string.IsNullOrEmpty(Config.DatabaseName)) throw new ArgumentException(nameof(MongoDBOptions.DatabaseName));
        if (string.IsNullOrEmpty(Config.CollectionName)) throw new ArgumentException(nameof(MongoDBOptions.CollectionName));

        var mongoClient = new MongoClient(
            _mongoDBOptions.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            _mongoDBOptions.Value.DatabaseName);

        _logCollection = mongoDatabase.GetCollection<BsonDocument>(_mongoDBOptions.Value.CollectionName);

        return _logCollection;
    }
    
    public async Task SendEventAsync(LogCollection body, CancellationToken cancellationToken = default)
    {
        
        cancellationToken.ThrowIfCancellationRequested();

        if (!Config.Enabled) return;

        var client = await GetClient();

        body.Index = Config.Index;
        body.Date = DateTime.UtcNow;
        
        var document = BsonSerializer.Deserialize<BsonDocument>(JsonConvert.SerializeObject(body));
        await client.InsertOneAsync(document);
    }
}