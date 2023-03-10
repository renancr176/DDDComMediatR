namespace ApiMvno.Infra.CrossCutting.Portability.Models.Requests;

public abstract class PortabilityBaseRequest
{
    public string RequestId { get; set; }

    protected PortabilityBaseRequest(string requestId)
    {
        RequestId = requestId;
    }
}