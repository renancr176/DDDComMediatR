namespace ApiMvno.Infra.CrossCutting.Jsc.Models;


public class JscMsisdnModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string StoreStatus { get; set; }
    public string GovernmentId { get; set; }
    public string GovernmentStatus { get; set; }
    public DateTime QuarantineStart { get; set; }
    public JscPoolModel PoolModel { get; set; }
    public JscSourcePoolModel SourcePool { get; set; }
    public JscSimModel SimModel { get; set; }
    public JscSubscriptionModel SubscriptionModel { get; set; }
}