namespace ApiMvno.Infra.CrossCutting.Portability.Models.Responses
{
    public class PortabilityBaseResponse
    {
        public bool Success => !Errors.Any();
        public List<PortabilityBaseResponseError>? Errors { get; set; } = new List<PortabilityBaseResponseError>();
    }

    public class PortabilityBaseResponseError
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }

    public class PortabilityBaseResponse<TData> : PortabilityBaseResponse
    {
        public TData Data { get; set; }
    }
}
