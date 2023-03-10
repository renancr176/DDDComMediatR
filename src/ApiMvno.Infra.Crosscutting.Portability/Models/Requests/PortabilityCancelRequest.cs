using ApiMvno.Infra.CrossCutting.Portability.Models.Enums;

namespace ApiMvno.Infra.CrossCutting.Portability.Models.Requests;

public class PortabilityCancelRequest
{
    public string PhoneNumber { get; set; }
    public CancellationCodeEnum TicketCancellationCode { get; set; }

    public PortabilityCancelRequest(string phoneNumber, CancellationCodeEnum ticketCancellationCode)
    {
        PhoneNumber = phoneNumber;
        TicketCancellationCode = ticketCancellationCode;
    }
}