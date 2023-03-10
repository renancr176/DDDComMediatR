
using ApiMvno.Infra.CrossCutting.BackServiceApi.Enums;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;

public class CreatePreSubscriptionResponse : BaseModel
{
    public Guid CompanyId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid AccountId { get; set; }
    public Guid CompanyCnId { get; set; }
    public Guid CompanySubscriptionTypeId { get; set; }
    public Guid CompanyNetworkProfileId { get; set; }
    public long StatusId { get; set; }
    public string IccId { get; set; }
    public long Msisdn { get; set; }
    public PreSubscriptionStatusEnum Status { get; set; }
    public string CustomerExternalCode { get; set; }
    public string AccountExternalCode { get; set; }
    public string? SubscriptionExternalCode { get; set; }
    public Guid? SubscriptionId { get; set; }
    public IEnumerable<CreatePreSubscriptionProductResponse> PreSubscriptionProducts { get; set; }
    public IEnumerable<PreSubscriptionHistoryModel> PreSubscriptionHistories { get; set; }
}
