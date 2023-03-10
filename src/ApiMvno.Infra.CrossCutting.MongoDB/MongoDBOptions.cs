using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ApiMvno.Infra.CrossCutting.MongoDB;

[ExcludeFromCodeCoverage]
public class MongoDBOptions
{
    public static string sectionKey = "MongoDB";

    public string? Index { get; set; } = $"{Assembly.GetExecutingAssembly().GetName().Name}-{new DateTime():MM-YYYY}";
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string? CollectionName { get; set; }
    public bool Enabled { get; set; }
}