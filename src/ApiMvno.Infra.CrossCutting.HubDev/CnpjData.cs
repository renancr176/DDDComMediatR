using Newtonsoft.Json;

namespace ApiMvno.Infra.CrossCutting.HubDev;

public class CnpjData
{
    [JsonProperty("numero_de_inscricao")]
    public string NumeroDeInscricao { get; set; }
    public string Tipo { get; set; }
    public string Abertura { get; set; }
    public string Nome { get; set; }
    public string Fantasia { get; set; }
    public string Porte { get; set; }
    [JsonProperty("atividade_principal")]
    public CnpjDataAtividade AtividadePrincipal { get; set; }
    [JsonProperty("atividades_secundarias")]
    public IEnumerable<CnpjDataAtividade> AtividadesSecundarias { get; set; }
    [JsonProperty("")]
    public string NaturezaJuridica { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Cep { get; set; }
    public string Bairro { get; set; }
    public string Municipio { get; set; }
    public string Uf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    [JsonProperty("entidade_federativo_responsavel")]
    public string EntidadeFederativoResponsavel { get; set; }
    public string Situacao { get; set; }
    [JsonProperty("motivo_situacao_cadastral")]
    public string MotivoSituacaoCadastral { get; set; }
    [JsonProperty("dt_situacao_cadastral")]
    public string DtSituacaoCadastral { get; set; }
    [JsonProperty("situacao_especial")]
    public string SituacaoEspecial { get; set; }
    [JsonProperty("data_situacao_especial")]
    public string DataSituacaoEspecial { get; set; }
    [JsonProperty("capital_social")]
    public string CapitalSocial { get; set; }
    [JsonProperty("quadro_socios")]
    public IEnumerable<string> QuadroSocios { get; set; }
    [JsonProperty("quadro_de_socios")]
    public IEnumerable<CnpjJQuadroSocios> QuadroDeSocios { get; set; }
}