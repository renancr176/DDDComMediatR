namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;

public class UpdateSubscriptionStatusRequest : JscAuthRequest
{
    public string SubscriptionId { get; set; }
    public string Status { get; set; }

    public UpdateSubscriptionStatusRequest(string userName, string password, string brandId, string subscriptionId,
        string status) 
        : base(userName, password, brandId)
    {
        SubscriptionId = subscriptionId;
        Status = status;
    }
}