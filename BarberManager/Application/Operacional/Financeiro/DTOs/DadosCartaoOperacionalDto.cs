namespace Application.Operacional.Financeiro.DTOs.Asaas;

public sealed record DadosCartaoOperacionalDto(
    string Numero,
    string NomeTitular,
    string MesValidade,
    string AnoValidade,
    string Cvv,
    string EmailTitular,
    string CpfCnpjTitular,
    string CepTitular,
    string NumeroEnderecoTitular,
    string TelefoneTitular);