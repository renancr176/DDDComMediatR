using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMvno.Infra.CrossCutting.MongoDB;

public static class MongoDBIoC
{
    public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDBOptions>(configuration.GetSection(MongoDBOptions.sectionKey));
        services.AddSingleton<IMongoDBClient, MongoDBClient>();
    }
}