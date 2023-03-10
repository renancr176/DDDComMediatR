namespace ApiMvno.Infra.CrossCutting.HubDev;

public interface IHubDevService
{
    Task<CpfResponse?> GetCPF(string cpf, string data);
    Task<CnpjResponse?> GetCNPJ(string cnpj);
    Task<CepResponse?> GetCEP(string cep);
}