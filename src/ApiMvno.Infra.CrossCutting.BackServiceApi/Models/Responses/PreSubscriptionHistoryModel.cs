
using ApiMvno.Infra.CrossCutting.BackServiceApi.Enums;

namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;

public class PreSubscriptionHistoryModel
{
    public Guid PreSubscriptionId { get; set; }
    public PreSubscriptionHistoryStatusEnum Status { get; set; }
    public string? Message { get; set; }
}
