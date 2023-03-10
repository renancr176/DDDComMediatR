using Newtonsoft.Json;

namespace ApiMvno.Infra.CrossCutting.Jsc.Models;

public class JscFeatureModel
{
    public string? Id { get; set; }
    public string Name { get; set; }
    [JsonProperty("featureID")]
    public string FeatureId { get; set; }
    public bool Active { get; set; }
    public bool ExtBool { get; set; }
    public int ExtNumber { get; set; }
    public string ExtString { get; set; }
    public bool UserModifiable { get; set; }
    public bool UserVisible { get; set; }
    public string FeatureModel { get; set; }
    public bool FeatureOnlyAdmin { get; set; }
}