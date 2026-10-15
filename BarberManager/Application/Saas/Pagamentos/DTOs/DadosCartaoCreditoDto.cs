using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public sealed record DadosCartaoCreditoDto(
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
}
