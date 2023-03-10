namespace ApiMvno.Infra.CrossCutting.HubDev;

public class CepResponse
{
	public bool Status { get; set; }
    public string Return { get; set; }
    public CepData Result { get; set; }
}