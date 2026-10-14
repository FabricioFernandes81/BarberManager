using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common.Enuns
{
    public enum StatusFatura
    {
        Pendente = 1,
        Paga = 2,
        Vencida = 3,
        Cancelada = 4,
        EmProcessamento = 5,
        FalhaPagamento = 6
    }
}
