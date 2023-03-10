namespace ApiMvno.Infra.CrossCutting.HubDev;

public class CpfResponse
{
    public bool Status { get; set; }
    public string? Return { get; set; }
    public CpfData Result { get; set; }
}