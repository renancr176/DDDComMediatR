namespace ApiMvno.Infra.CrossCutting.Jsc.Models;

public class JscConsumptionDetailModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public JscSubscriptionModel Subscription { get; set; }
    public string Type { get; set; }
    public string PublicIdentity { get; set; }
    public string Originator { get; set; }
    public string Destination { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ParseDate { get; set; }
    public int TrafficUnits { get; set; }
    public string Concept { get; set; }
    public JscPackageModel Pkg { get; set; }
    public JscPackageModel PackageInstance { get; set; }
    public string RatingPackRef { get; set; }
    public string SubscriptionLocation { get; set; }
    public string ServiceOriginatorPattern { get; set; }
    public string DestinationPattern { get; set; }
    public string ReleaseReason { get; set; }
    public string SessionId { get; set; }
}