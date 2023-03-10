namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;

public class CreatePreSubscriptionProductResponse
{
    public Guid PreSubscriptionId { get; set; }
    public Guid ProductId { get; set; }
    public DateTime InitialDate { get; set; }
    public DateTime? FinalDate { get; set; }
    public string ProductExternalCode { get; set; }
}
