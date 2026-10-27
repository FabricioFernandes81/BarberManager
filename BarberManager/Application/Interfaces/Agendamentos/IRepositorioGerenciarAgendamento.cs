using Application.Operacional.Agendamentos.DTOs;
using Application.Operacional.Agendamentos.Queries;
using Domain.Entities.Agendamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Agendamentos
{
    public interface IRepositorioGerenciarAgendamento
    {
        Task<Agendamento?> ObterParaAtualizacaoAsync(
                        Guid empresaId,Guid agendamentoId, CancellationToken cancellationToken);


        Task<ObterAgendaPeriodoDto> ObterAgendaPeriodoAsync(
               Guid empresaId,
               ObterAgendaPeriodoQuery query,
               CancellationToken cancellationToken);
    }
}
