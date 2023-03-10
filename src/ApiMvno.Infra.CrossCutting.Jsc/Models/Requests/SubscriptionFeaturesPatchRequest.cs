using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;

public class SubscriptionFeaturesPatchRequest : JscAuthRequest
{
    public string SubscriptionId { get; set; }
    public IEnumerable<SubscriptionFeaturesRequest> Features { get; set; }

    public SubscriptionFeaturesPatchRequest(string userName, string password, string brandId, string subscriptionId,
        IEnumerable<SubscriptionFeaturesRequest> features) 
        : base(userName, password, brandId)
    {
        SubscriptionId = subscriptionId;
        Features = features;
    }
}

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public class SubscriptionFeaturesRequest
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public bool ExtBool { get; set; }
    public int ExtNumber { get; set; }
    public string ExtString { get; set; }

    public SubscriptionFeaturesRequest(string? id, string name, bool active, bool extBool, int extNumber,
        string extString)
    {
        Id = id;
        Name = name;
        Active = active;
        ExtBool = extBool;
        ExtNumber = extNumber;
        ExtString = extString;
    }
}
