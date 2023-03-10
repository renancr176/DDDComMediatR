namespace ApiMvno.Infra.CrossCutting.Jsc.Models;

public class JscSimCardModel
{
    public string SimCode { get; set; }
    public string Puk { get; set; }
    public string Status { get; set; }
    public string StoreStatus { get; set; }
    public JscSubscriptionModel Subscription { get; set; }
    public JscImsiModel Imsi { get; set; }
}
