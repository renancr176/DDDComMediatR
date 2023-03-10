using ApiMvno.Infra.CrossCutting.Jsc.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;

public abstract class PagedRequest : JscAuthRequest
{
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 20;
    public string OrderBy { get; set; } = "name";
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderModeEnum OrderMode { get; set; } = OrderModeEnum.ASC;

    public PagedRequest(string userName, string password, string brandId)
        : base(userName, password, brandId)
    {
    }

    protected PagedRequest(string userName,
        string password,
        string brandId,
        int page,
        int size)
        : this(userName, password, brandId)
    {
        Page = page;
        Size = size;
    }

    protected PagedRequest(string userName,
        string password,
        string brandId,
        int page,
        int size,
        string orderBy,
        OrderModeEnum orderMode) 
        : this(userName, password, brandId, page, size)
    {
        OrderBy = orderBy;
        OrderMode = orderMode;
    }
}