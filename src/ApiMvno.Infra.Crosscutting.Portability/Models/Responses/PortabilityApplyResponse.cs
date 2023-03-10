using ApiMvno.Infra.CrossCutting.Portability.Models.Enums;

namespace ApiMvno.Infra.CrossCutting.Portability.Models.Responses;

public class PortabilityApplyResponse
{
    public Guid ProtocolId { get; set; }
    public StatusRequestEnum StatusRequest { get; set; }
    public List<Tickets> Tickets { get; set; }
}

public class Tickets
{
    public StatusPortabilityEnum StatusPortability { get; set; }
    public string PhoneNumber { get; set; }
    public bool Success { get; set; }
    public string? Error { get; set; }
}