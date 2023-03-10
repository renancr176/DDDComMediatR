
namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Requests;

public class JscSimCardRequest : JscAuthRequest
{
    public string Iccid { get; set; }

    public JscSimCardRequest(string userName, string password, string brandId, string iccid)
      : base(userName, password, brandId)
    {
        Iccid = iccid;
    }
}
