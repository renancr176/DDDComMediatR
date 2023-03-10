using Newtonsoft.Json;

namespace ApiMvno.Infra.CrossCutting.HubDev;

public class CnpjJQuadroSocios
{
    public string Informacoes { get; set; }
    public string Nome { get; set; }
    public string Qualificacao { get; set; }
    [JsonProperty("pais_de_origem")]
    public string PaisDeOrigem { get; set; }
    [JsonProperty("nome_representante_legal")]
    public string NomeRepresentanteLegal { get; set; }
    [JsonProperty("qualificacao_representante_legal")]
    public string QualificacaoRepresentanteLegal { get; set; }
}