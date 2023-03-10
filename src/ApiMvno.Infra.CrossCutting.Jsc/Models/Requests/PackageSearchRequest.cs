using ApiMvno.Infra.CrossCutting.Jsc.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;

public class PackageSearchRequest : PagedRequest
{
    public string? PackageIdList { get; set; }
    public string? PkgType { get; set; }
    public string? PkgCategory { get; set; }
    public string? Subcategory { get; set; }
    public string? Family { get; set; }
    public string? SubFamily { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public PackageStatusEnum? Status { get; set; }
    public string? BillingType { get; set; }
    public string? PortaType { get; set; }
    public string? Scope { get; set; }
    public DateTime? PublishDateEq { get; set; }
    public DateTime? PublishDateFrom { get; set; }
    public DateTime? PublishDateTo { get; set; }
    public DateTime? UnpublishDateEq { get; set; }
    public DateTime? UnpublishDateFrom { get; set; }
    public DateTime? UnpublishDateTo { get; set; }

    public PackageSearchRequest(string userName, string password, string brandId)
        : base(userName, password, brandId)
    {
    }

    public PackageSearchRequest(string userName,
        string password,
        string brandId,
        int page,
        int size)
        : base(userName, password, brandId, page, size)
    {
    }

    public PackageSearchRequest(string userName,
        string password,
        string brandId,
        int page,
        int size,
        string orderBy,
        OrderModeEnum orderMode)
        : base(userName, password, brandId, page, size, orderBy, orderMode)
    {
    }

    public PackageSearchRequest(string userName,
        string password,
        string brandId,
        int page,
        int size,
        string orderBy,
        OrderModeEnum orderMode,
        string? packageIdList = null,
        string? pkgType = null,
        string? pkgCategory = null,
        string? subcategory = null,
        string? family = null,
        string? subFamily = null,
        PackageStatusEnum? status = null,
        string? billingType = null,
        string? portaType = null,
        string? scope = null,
        DateTime? publishDateEq = null,
        DateTime? publishDateFrom = null,
        DateTime? publishDateTo = null,
        DateTime? unpublishDateEq = null,
        DateTime? unpublishDateFrom = null,
        DateTime? unpublishDateTo = null)
        : base(userName, password, brandId, page, size, orderBy, orderMode)
    {
        PackageIdList = packageIdList;
        PkgType = pkgType;
        PkgCategory = pkgCategory;
        Subcategory = subcategory;
        Family = family;
        SubFamily = subFamily;
        Status = status;
        BillingType = billingType;
        PortaType = portaType;
        Scope = scope;
        PublishDateEq = publishDateEq;
        PublishDateFrom = publishDateFrom;
        PublishDateTo = publishDateTo;
        UnpublishDateEq = unpublishDateEq;
        UnpublishDateFrom = unpublishDateFrom;
        UnpublishDateTo = unpublishDateTo;
    }

    public PackageSearchRequest(string userName,
        string password,
        string brandId,
        string? packageIdList = null,
        string? pkgType = null,
        string? pkgCategory = null,
        string? subcategory = null,
        string? family = null,
        string? subFamily = null,
        PackageStatusEnum? status = null,
        string? billingType = null,
        string? portaType = null,
        string? scope = null,
        DateTime? publishDateEq = null,
        DateTime? publishDateFrom = null,
        DateTime? publishDateTo = null,
        DateTime? unpublishDateEq = null,
        DateTime? unpublishDateFrom = null,
        DateTime? unpublishDateTo = null) 
        : this(userName,
            password,
            brandId,
            0,
            20,
            packageIdList,
            pkgType,
            pkgCategory,
            subcategory,
            family,
            subFamily,
            status,
            billingType,
            portaType,
            scope,
            publishDateEq,
            publishDateFrom,
            publishDateTo,
            unpublishDateEq,
            unpublishDateFrom,
            unpublishDateTo)
    {
    }

    public PackageSearchRequest(string userName,
        string password,
        string brandId,
        int page,
        int size,
        string? packageIdList = null,
        string? pkgType = null,
        string? pkgCategory = null,
        string? subcategory = null,
        string? family = null,
        string? subFamily = null,
        PackageStatusEnum? status = null,
        string? billingType = null,
        string? portaType = null,
        string? scope = null,
        DateTime? publishDateEq = null,
        DateTime? publishDateFrom = null,
        DateTime? publishDateTo = null,
        DateTime? unpublishDateEq = null,
        DateTime? unpublishDateFrom = null,
        DateTime? unpublishDateTo = null)
        : this(userName,
            password,
            brandId,
            page,
            size,
            "name",
            OrderModeEnum.ASC,
            packageIdList,
            pkgType,
            pkgCategory,
            subcategory,
            family,
            subFamily,
            status,
            billingType,
            portaType,
            scope,
            publishDateEq,
            publishDateFrom,
            publishDateTo,
            unpublishDateEq,
            unpublishDateFrom,
            unpublishDateTo)
    {
    }
}