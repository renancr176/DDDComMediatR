using ApiMvno.Infra.CrossCutting.Jsc.Models.Enums;

namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;

public class SubscriptionFeaturesGetRequest : PagedRequest
{
    public string SubscriptionId { get; set; }

    public SubscriptionFeaturesGetRequest(string userName, string password, string brandId, string subscriptionId) 
        : base(userName, password, brandId)
    {
        SubscriptionId = subscriptionId;
    }

    public SubscriptionFeaturesGetRequest(string userName, string password, string brandId, int page, int size, string subscriptionId) 
        : base(userName, password, brandId, page, size)
    {
        SubscriptionId = subscriptionId;
    }

    public SubscriptionFeaturesGetRequest(string userName, string password, string brandId, int page, int size, string orderBy, OrderModeEnum orderMode, string subscriptionId) 
        : base(userName, password, brandId, page, size, orderBy, orderMode)
    {
        SubscriptionId = subscriptionId;
    }
}