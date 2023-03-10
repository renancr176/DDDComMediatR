namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests;

public abstract class BackServiceBaseRequest
{
    public string RequestId { get; set; }

    protected BackServiceBaseRequest(string requestId)
    {
        RequestId = requestId;
    }
}