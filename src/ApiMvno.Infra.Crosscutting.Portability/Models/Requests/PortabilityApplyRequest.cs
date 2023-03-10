using ApiMvno.Infra.CrossCutting.Portability.Models.Enums;

namespace ApiMvno.Infra.CrossCutting.Portability.Models.Requests;

public class PortabilityApplyRequest
{
    public List<string> PhoneNumberList { get; set; }
    public string CustomerName { get; set; }
    public string CustomerDocument { get; set; }
    public CustomerTypeEnum CustomerType { get; set; }
    public DateTime? DateScheduling { get; set; }
    public bool NextWindowAvailable { get; set; } = true;
    public string Mvno { get; set; }

    public PortabilityApplyRequest(List<string> phoneNumberList, string customerName, string customerDocument,
        CustomerTypeEnum customerType, DateTime? dateScheduling, bool nextWindowAvailable, string mvno)
    {
        PhoneNumberList = phoneNumberList;
        CustomerName = customerName;
        CustomerDocument = customerDocument;
        CustomerType = customerType;
        DateScheduling = dateScheduling;
        NextWindowAvailable = nextWindowAvailable;
        Mvno = mvno;
    }
}