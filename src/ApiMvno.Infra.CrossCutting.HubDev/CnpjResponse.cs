namespace ApiMvno.Infra.CrossCutting.HubDev;

public class CnpjResponse
{
    public bool Status { get; set; }
    public string? Return { get; set; }
    public CnpjData Result { get; set; }
    public string? Message { get; set; }
}