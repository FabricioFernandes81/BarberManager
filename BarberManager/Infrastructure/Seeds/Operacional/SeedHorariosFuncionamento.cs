using Domain.Entities.Agendamentos;
using Infrastructure.Seeds.Empresas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Operacional
{
    public static class SeedHorariosFuncionamento
    {
        private static readonly Guid Segunda =
            Guid.Parse("81000000-0000-0000-0000-000000000001");

        private static readonly Guid Terca =
            Guid.Parse("81000000-0000-0000-0000-000000000002");

        private static readonly Guid Quarta =
            Guid.Parse("81000000-0000-0000-0000-000000000003");

        private static readonly Guid Quinta =
            Guid.Parse("81000000-0000-0000-0000-000000000004");

        private static readonly Guid Sexta =
            Guid.Parse("81000000-0000-0000-0000-000000000005");

        private static readonly Guid Sabado =
            Guid.Parse("81000000-0000-0000-0000-000000000006");

        public static void InicializarSeed(ModelBuilder builder)
        {
            builder.Entity<HorarioFuncionamento>().HasData(
                Criar(Segunda, DayOfWeek.Monday, 8, 18),
                Criar(Terca, DayOfWeek.Tuesday, 8, 18),
                Criar(Quarta, DayOfWeek.Wednesday, 8, 18),
                Criar(Quinta, DayOfWeek.Thursday, 8, 18),
                Criar(Sexta, DayOfWeek.Friday, 8, 18),
                Criar(Sabado, DayOfWeek.Saturday, 8, 13)
            );
        }

        private static HorarioFuncionamento Criar(
            Guid id,
            DayOfWeek dia,
            int horaInicio,
            int horaFim)
        {
            return new HorarioFuncionamento
            {
                Id = id,
                EmpresaId = SeedEmpresa.Empresa1,
                DiaSemana = dia,
                HoraInicio = new TimeOnly(horaInicio, 0),
                HoraFim = new TimeOnly(horaFim, 0),
                Ativo = true
            };
        }
    }
}
