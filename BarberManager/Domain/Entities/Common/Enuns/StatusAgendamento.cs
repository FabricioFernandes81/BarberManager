using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common.Enuns
{
    public enum StatusAgendamento
    {
        Pendente = 1,
        Confirmado = 2,
        EmAtendimento = 3,
        Concluido = 4,
        Cancelado = 5,
        NaoCompareceu = 6
    }
}
