namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;

public class ConsumptionTotalsRequest : JscAuthRequest
{
    public string SubscriptionId { get; set; }
    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
    public string? CdrServiceType { get; set; }
    public string? PackageId { get; set; }
    public string? Direction { get; set; }

    public ConsumptionTotalsRequest(string userName, string password, string brandId, string subscriptionId,
        DateTime? startDateFrom = null, DateTime? startDateTo = null, string? cdrServiceType = null,
        string? packageId = null, string? direction = null) 
        : base(userName, password, brandId)
    {
        SubscriptionId = subscriptionId;
        StartDateFrom = startDateFrom;
        StartDateTo = startDateTo;
        CdrServiceType = cdrServiceType;
        PackageId = packageId;
        Direction = direction;
    }
}