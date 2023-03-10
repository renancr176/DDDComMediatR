using ApiMvno.Infra.CrossCutting.Portability.Models.Enums;

namespace ApiMvno.Infra.CrossCutting.Portability.Models.Responses;

public class PortabilityCancelResponse
{
    public Guid ProtocolId { get; set; }
    public string PhoneNumber { get; set; }
    public StatusRequestEnum StatusRequest { get; set; }
}