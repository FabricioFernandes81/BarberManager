using Domain.Entities.Profissionais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Agendamentos
{
    public class HorarioProfissional
    {
        public Guid Id { get; private set; }

        public Guid EmpresaId { get; private set; }

        public Guid ProfissionalId { get; private set; }

        public DayOfWeek DiaSemana { get; private set; }

        public TimeOnly HoraInicio { get; private set; }

        public TimeOnly HoraFim { get; private set; }

        public bool Ativo { get; private set; }

        // Navegações
        public Profissional Profissional { get; private set; } = null!;

        protected HorarioProfissional()
        {
        }

        public HorarioProfissional(
            Guid empresaId,
            Guid profissionalId,
            DayOfWeek diaSemana,
            TimeOnly horaInicio,
            TimeOnly horaFim)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException(
                    "A empresa deve ser informada.", nameof(empresaId));

            if (profissionalId == Guid.Empty)
                throw new ArgumentException(
                    "O profissional deve ser informado.", nameof(profissionalId));

            if (horaFim <= horaInicio)
                throw new ArgumentException(
                    "O horário final deve ser posterior ao horário inicial.");

            Id = Guid.NewGuid();
            EmpresaId = empresaId;
            ProfissionalId = profissionalId;
            DiaSemana = diaSemana;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            Ativo = true;
        }

        public void AtualizarHorario(
            TimeOnly horaInicio,
            TimeOnly horaFim)
        {
            if (horaFim <= horaInicio)
                throw new ArgumentException(
                    "O horário final deve ser posterior ao horário inicial.");

            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;
    }
}
