using Microsoft.Extensions.Options;
using Renci.SshNet;

namespace ApiMvno.Infra.CrossCutting.Service555;

public class Service555Service : IService555Service
{
    private readonly IOptions<Service555Option> _options;

    public Service555Service(IOptions<Service555Option> options)
    {
        _options = options;
    }

    private Service555Option Config => _options.Value;

    private ConnectionInfo Connection => new ConnectionInfo(
        Config.Ip,
        Config.UserName,
        new PasswordAuthenticationMethod(
            Config.UserName,
            Config.Password));

    private const string ConfigError =
        $"The configuration #SECTIONKEY is not right configure into the appsettings.";

    /// <summary>
    /// Add 555 serviçe to a MSISDN
    /// </summary>
    /// <param name="agente">MVNO/DEALER CODE</param>
    /// <param name="numeroIn">MSISDN</param>
    /// <returns></returns>
    public string? Add(string agente, string numeroIn)
    {
        if (Config == null || (Config.Active && !Config.Ok()))
        {
            throw new ArgumentNullException(ConfigError.Replace("#SECTIONKEY", nameof(Service555Option.sectionKey)));
        }

        if (Config.Active)
        {
            using (var client = new SshClient(Connection))
            {
                client.Connect();

                var cmd = client.RunCommand("Scripts/Add_MVNO.sh " + agente + ' ' + numeroIn);
                var output = cmd.Result;

                return output;
            }
        }

        return default;
    }

    /// <summary>
    /// Remove 555 service from a MSISDN
    /// </summary>
    /// <param name="agente">MVNO/DEALER CODE</param>
    /// <param name="numeroIn">MSISDN</param>
    /// <returns></returns>
    public string? Remove(string agente, string numeroIn)
    {
        if (Config == null || (Config.Active && !Config.Ok()))
        {
            throw new ArgumentNullException(ConfigError.Replace("#SECTIONKEY", nameof(Service555Option.sectionKey)));
        }

        if (Config.Active)
        {
            using (var client = new SshClient(Connection))
            {
                client.Connect();
                //var cmd = client.RunCommand("Roteamento_NUN_Externo.sh EXTERNO_0800_A 08003332222 2130300555 5521972961074");
                //var cmd = client.RunCommand("Scripts/Add_Fluke.sh " + numero.Trim());
                var cmd = client.RunCommand("Scripts/Del_MVNO.sh " + agente + ' ' + numeroIn);
                var output = cmd.Result;

                return output;
            }
        }

        return default;
    }
}