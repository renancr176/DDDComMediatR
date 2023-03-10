namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;

public class CreatePreSubscriptionRequest : BackServiceBaseRequest
{
    public Guid CompanyId { get; set; }
    public Guid AccountId { get; set; }
    public Guid CompanyCnId { get; set; }
    public Guid CompanySubscriptionTypeId { get; set; }
    public Guid CompanyNetworkProfileId { get; set; }
    public long StatusId { get; set; }
    public string IccId { get; set; }
    public long Msisdn { get; set; }
    public string CustomerExternalCode { get; set; }
    public string AccountExternalCode { get; set; }
    public IEnumerable<CreatePreSubscriptionProductRequest> PreSubscriptionProducts { get; set; }

    public CreatePreSubscriptionRequest(string requestId, Guid companyId, Guid accountId, Guid companyCnId,
        Guid companySubscriptionTypeId, Guid companyNetworkProfileId, long statusId, string iccId, long msisdn,
        string customerExternalCode, string accountExternalCode,
        IEnumerable<CreatePreSubscriptionProductRequest> preSubscriptionProducts) 
        : base(requestId)
    {
        CompanyId = companyId;
        AccountId = accountId;
        CompanyCnId = companyCnId;
        CompanySubscriptionTypeId = companySubscriptionTypeId;
        CompanyNetworkProfileId = companyNetworkProfileId;
        StatusId = statusId;
        IccId = iccId;
        Msisdn = msisdn;
        CustomerExternalCode = customerExternalCode;
        AccountExternalCode = accountExternalCode;
        PreSubscriptionProducts = preSubscriptionProducts;
    }
}
