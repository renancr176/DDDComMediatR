using ApiMvno.Infra.CrossCutting.Jsc.Models.Enums;

namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;

public class ConsumptionDetailsRequest : PagedRequest
{
    public string SubscriptionId { get; set; }
    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
    public string? CdrDetailType { get; set; }
    public bool? AggregateCallsMo { get; set; }
    public bool? FilterHiddenMsisdn { get; set; }

    public ConsumptionDetailsRequest(string userName, string password, string brandId) 
        : base(userName, password, brandId)
    {
    }

    public ConsumptionDetailsRequest(string userName, string password, string brandId, int page, int size) 
        : base(userName, password, brandId, page, size)
    {
    }

    public ConsumptionDetailsRequest(string userName, string password, string brandId, int page, int size,
        string orderBy, OrderModeEnum orderMode) 
        : base(userName, password, brandId, page, size, orderBy, orderMode)
    {
    }

    public ConsumptionDetailsRequest(string userName, string password, string brandId, string subscriptionId,
        DateTime? startDateFrom = null, DateTime? startDateTo = null, string? cdrDetailType = null,
        bool? aggregateCallsMo = null, bool? filterHiddenMsisdn = null)
        : base(userName, password, brandId)
    {
        SubscriptionId = subscriptionId;
        StartDateFrom = startDateFrom;
        StartDateTo = startDateTo;
        CdrDetailType = cdrDetailType;
        AggregateCallsMo = aggregateCallsMo;
        FilterHiddenMsisdn = filterHiddenMsisdn;
    }

    public ConsumptionDetailsRequest(string userName, string password, string brandId, int page, int size,
        string subscriptionId, DateTime? startDateFrom = null, DateTime? startDateTo = null,
        string? cdrDetailType = null, bool? aggregateCallsMo = null, bool? filterHiddenMsisdn = null)
        : this(userName, password, brandId, page, size)
    {
        SubscriptionId = subscriptionId;
        StartDateFrom = startDateFrom;
        StartDateTo = startDateTo;
        CdrDetailType = cdrDetailType;
        AggregateCallsMo = aggregateCallsMo;
        FilterHiddenMsisdn = filterHiddenMsisdn;
    }

    public ConsumptionDetailsRequest(string userName, string password, string brandId, int page, int size,
        string orderBy, OrderModeEnum orderMode, string subscriptionId, DateTime? startDateFrom = null,
        DateTime? startDateTo = null, string? cdrDetailType = null, bool? aggregateCallsMo = null,
        bool? filterHiddenMsisdn = null)
        : base(userName, password, brandId, page, size, orderBy, orderMode)
    {
        SubscriptionId = subscriptionId;
        StartDateFrom = startDateFrom;
        StartDateTo = startDateTo;
        CdrDetailType = cdrDetailType;
        AggregateCallsMo = aggregateCallsMo;
        FilterHiddenMsisdn = filterHiddenMsisdn;
    }
}
