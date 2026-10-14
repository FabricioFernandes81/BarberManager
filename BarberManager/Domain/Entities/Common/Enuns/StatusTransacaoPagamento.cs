using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common.Enuns
{
    public enum StatusTransacaoPagamento
    {
        Criada = 1,
        Pendente = 2,
        EmProcessamento = 3,
        Aprovada = 4,
        Recusada = 5,
        Cancelada = 6,
        Estornada = 7,
        Expirada = 8
    }
}
