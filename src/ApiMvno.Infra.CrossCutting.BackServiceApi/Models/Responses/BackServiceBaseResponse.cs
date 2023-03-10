namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Responses;
public class BackServiceBaseResponse
{
    public bool Success => !Errors.Any();
    public List<BackServiceBaseResponseError> Errors { get; set; } = new List<BackServiceBaseResponseError>();
}

public class BackServiceBaseResponseError
{
    public string ErrorCode { get; set; }
    public string Message { get; set; }
}

public class BackServiceBaseResponse<T> : BackServiceBaseResponse
{
    public T Data { get; set; }
}