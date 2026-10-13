using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common.Enuns
{
    public enum StatusAssinaturaCliente
    {
        Pendente = 1,
        Ativa = 2,
        Suspensa = 3,
        Cancelada = 4,
        Expirada = 5,
        Inadimplente = 6
    }
}
