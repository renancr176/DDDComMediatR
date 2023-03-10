namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;

public class CreatePreSubscriptionProductRequest
{
    public Guid ProductId { get; set; }
    public DateTime InitialDate { get; set; }
    public DateTime? FinalDate { get; set; }
    public string ProductExternalCode { get; set; }

    public CreatePreSubscriptionProductRequest(Guid productId, DateTime initialDate, DateTime? finalDate,
        string productExternalCode)
    {
        ProductId = productId;
        InitialDate = initialDate;
        FinalDate = finalDate;
        ProductExternalCode = productExternalCode;
    }
}
