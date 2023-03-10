using ApiMvno.Infra.CrossCutting.Jsc.Models.Enums;

namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;

public class MsisdnSearchRequest : PagedRequest
{
    public string? Subscription { get; set; }
    public string? Pool { get; set; }
    public string? SourcePool { get; set; }
    public string? Status { get; set; }
    public string? StoreStatus { get; set; }
    public string? Msisdn { get; set; }
    public DateTime? QuarantineEq { get; set; }
    public DateTime? QuarantineFrom { get; set; }
    public DateTime? QuarantineTo { get; set; }

    public MsisdnSearchRequest(string userName, string password, string brandId)
        : base(userName, password, brandId)
    {
    }

    public MsisdnSearchRequest(string userName,
        string password,
        string brandId,
        int page,
        int size)
        : base(userName, password, brandId, page, size)
    {
    }

    public MsisdnSearchRequest(string userName,
        string password,
        string brandId,
        int page,
        int size,
        string orderBy,
        OrderModeEnum orderMode)
        : base(userName, password, brandId, page, size, orderBy, orderMode)
    {
    }

    public MsisdnSearchRequest(
        string userName,
        string password,
        string brandId,
        int page,
        int size,
        string orderBy,
        OrderModeEnum orderMode,
        string? subscription,
        string? pool,
        string? sourcePool,
        string? status,
        string? storeStatus ,
        string? msisdn,
        DateTime? quarantineEq = null,
        DateTime? quarantineFrom = null,
        DateTime? quarantineTo = null)
        : base(userName, password, brandId, page, size, orderBy, orderMode)
    {
        Subscription = subscription;
        Pool = pool;
        SourcePool = sourcePool;
        Status = status;
        StoreStatus = storeStatus;
        Msisdn = msisdn;
        QuarantineEq = quarantineEq;
        QuarantineFrom = quarantineFrom;
        QuarantineTo = quarantineTo;
    }

    public MsisdnSearchRequest(string userName,
        string password,
        string brandId,
        string? subscription = null,
        string? pool = null,
        string? sourcePool = null,
        string? status = null,
        string? storeStatus = null,
        string? msisdn = null,
        DateTime? quarantineEq = null,
        DateTime? quarantineFrom = null,
        DateTime? quarantineTo = null) 
        : this(userName,
            password,
            brandId,
            0,
            20,
            subscription,
            pool,
            sourcePool,
            status,
            storeStatus,
            msisdn,
            quarantineEq,
            quarantineFrom,
            quarantineTo)
    {
    }

    public MsisdnSearchRequest(string userName,
        string password,
        string brandId,
        int page,
        int size,
        string? subscription = null,
        string? pool = null,
        string? sourcePool = null,
        string? status = null,
        string? storeStatus = null,
        string? msisdn = null,
        DateTime? quarantineEq = null,
        DateTime? quarantineFrom = null,
        DateTime? quarantineTo = null)
        : this(userName,
            password,
            brandId,
            page,
            size,
            "name",
            OrderModeEnum.ASC,
            subscription,
            pool,
            sourcePool,
            status,
            storeStatus,
            msisdn,
            quarantineEq,
            quarantineFrom,
            quarantineTo)
    {
    }

    public MsisdnSearchRequest(string userName,
        string password,
        string brandId,
        string msisdn,
        int page = 0,
        int size = 5
        )
        : base(userName, password, brandId, page, size)
    {
        Msisdn = msisdn; // TO DO: Alterar para Cn.
    }
}