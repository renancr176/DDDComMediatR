using Newtonsoft.Json;

namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;

public class JscBaseResponse
{
    public bool Success => !Errors.Any();
    public List<JscBaseResponseError> Errors { get; set; } = new List<JscBaseResponseError>();
}

public class JscBaseResponse<TData> : JscBaseResponse
{
    [JsonProperty("content")]
    public TData Data { get; set; }
}

public class JscBaseResponseError
{
    public string Code { get; set; }
    public string? Message { get; set; }
}