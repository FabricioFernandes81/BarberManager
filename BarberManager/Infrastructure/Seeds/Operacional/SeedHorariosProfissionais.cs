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
    public static class SeedHorariosProfissionais
    {
        public static void InicializarSeed(ModelBuilder builder)
        {
            builder.Entity<HorarioProfissional>().HasData(
                // Profissional 1 — segunda a sexta, manhã e tarde
                Criar(
                    "82000000-0000-0000-0000-000000000001",
                    SeedProfissionais.Profissional1,
                    DayOfWeek.Monday, 9, 12),
                Criar(
                    "82000000-0000-0000-0000-000000000002",
                    SeedProfissionais.Profissional1,
                    DayOfWeek.Monday, 13, 18),

                Criar(
                    "82000000-0000-0000-0000-000000000003",
                    SeedProfissionais.Profissional1,
                    DayOfWeek.Tuesday, 9, 12),
                Criar(
                    "82000000-0000-0000-0000-000000000004",
                    SeedProfissionais.Profissional1,
                    DayOfWeek.Tuesday, 13, 18),

                Criar(
                    "82000000-0000-0000-0000-000000000005",
                    SeedProfissionais.Profissional1,
                    DayOfWeek.Wednesday, 9, 12),
                Criar(
                    "82000000-0000-0000-0000-000000000006",
                    SeedProfissionais.Profissional1,
                    DayOfWeek.Wednesday, 13, 18),

                Criar(
                    "82000000-0000-0000-0000-000000000007",
                    SeedProfissionais.Profissional1,
                    DayOfWeek.Thursday, 9, 12),
                Criar(
                    "82000000-0000-0000-0000-000000000008",
                    SeedProfissionais.Profissional1,
                    DayOfWeek.Thursday, 13, 18),

                Criar(
                    "82000000-0000-0000-0000-000000000009",
                    SeedProfissionais.Profissional1,
                    DayOfWeek.Friday, 9, 12),
                Criar(
                    "82000000-0000-0000-0000-000000000010",
                    SeedProfissionais.Profissional1,
                    DayOfWeek.Friday, 13, 18),

                // Profissional 2 — segunda a sexta, manhã e tarde
                Criar(
                    "82000000-0000-0000-0000-000000000011",
                    SeedProfissionais.Profissional2,
                    DayOfWeek.Monday, 8, 12),
                Criar(
                    "82000000-0000-0000-0000-000000000012",
                    SeedProfissionais.Profissional2,
                    DayOfWeek.Monday, 13, 18),

                Criar(
                    "82000000-0000-0000-0000-000000000013",
                    SeedProfissionais.Profissional2,
                    DayOfWeek.Tuesday, 8, 12),
                Criar(
                    "82000000-0000-0000-0000-000000000014",
                    SeedProfissionais.Profissional2,
                    DayOfWeek.Tuesday, 13, 18),

                Criar(
                    "82000000-0000-0000-0000-000000000015",
                    SeedProfissionais.Profissional2,
                    DayOfWeek.Wednesday, 8, 12),
                Criar(
                    "82000000-0000-0000-0000-000000000016",
                    SeedProfissionais.Profissional2,
                    DayOfWeek.Wednesday, 13, 18),

                Criar(
                    "82000000-0000-0000-0000-000000000017",
                    SeedProfissionais.Profissional2,
                    DayOfWeek.Thursday, 8, 12),
                Criar(
                    "82000000-0000-0000-0000-000000000018",
                    SeedProfissionais.Profissional2,
                    DayOfWeek.Thursday, 13, 18),

                Criar(
                    "82000000-0000-0000-0000-000000000019",
                    SeedProfissionais.Profissional2,
                    DayOfWeek.Friday, 8, 12),
                Criar(
                    "82000000-0000-0000-0000-000000000020",
                    SeedProfissionais.Profissional2,
                    DayOfWeek.Friday, 13, 18)
            );
        }

        private static object Criar(
            string id,
            Guid profissionalId,
            DayOfWeek dia,
            int horaInicio,
            int horaFim)
        {
            return new
            {
                Id = Guid.Parse(id),
                EmpresaId = SeedEmpresa.Empresa1,
                ProfissionalId = profissionalId,
                DiaSemana = dia,
                HoraInicio = new TimeOnly(horaInicio, 0),
                HoraFim = new TimeOnly(horaFim, 0),
                Ativo = true
            };
        }
    }
}
