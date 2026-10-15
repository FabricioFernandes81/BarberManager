using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public class GerarFaturaRequest
    {
        Guid _AssinaturaId;
        DateTime _PeriodoInicio;
        DateTime _PeriodoFim;
        DateTime _Vencimento;

        public GerarFaturaRequest(Guid assinatura, DateTime periodoInicio, DateTime periodoFim,
            DateTime vencimento)
        {
            _AssinaturaId = assinatura;
            _PeriodoInicio = periodoInicio;
            _PeriodoFim = periodoFim;
            _Vencimento = vencimento;
        }

        public Guid Assinatura
        {
            set { _AssinaturaId = value; }
            get { return _AssinaturaId; }
        }
        public DateTime PeriodoInicio
        {
            set { _PeriodoInicio = value; }
            get { return _PeriodoInicio; }
        }
        public DateTime PeriodoFim
        {
            set { _PeriodoFim = value; }
            get { return _PeriodoFim; }
        }
        public DateTime Vencimento
        {
            set { _Vencimento = value; }
            get { return _Vencimento;  }
        }

    }
}
