using Newtonsoft.Json;

namespace ApiMvno.Infra.CrossCutting.HubDev;

public class CpfData
{
    [JsonProperty("numero_de_cpf")]
    public string NumeroDeCpf { get; set; }
    [JsonProperty("nome_da_pf")]
    public string NomeDaPf { get; set; }
    [JsonProperty("data_nascimento")]
    public string DataNascimento { get; set; }
    [JsonProperty("situacao_cadastral")]
    public string SituacaoCadastral { get; set; }
    [JsonProperty("data_inscricao")]
    public string DataInscricao { get; set; }
    [JsonProperty("digito_verificador")]
    public string DigitoVerificador { get; set; }
    [JsonProperty("comprovante_emitido")]
    public string ComprovanteEmitido { get; set; }
    [JsonProperty("comprovante_emitido_data")]
    public string ComprovanteEmitidoData { get; set; }
}