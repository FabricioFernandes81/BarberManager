using Application.Interfaces.Empresas;
using Application.Saas.Empresas;
using Application.Saas.Pagamentos.Commands;
using Application.Saas.Pagamentos.Commands.Handlers;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using Infrastructure.Context;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Moq;

namespace Tests.Integration
{
    public sealed class TrialExpirationServiceTests
    {
        private readonly DbContextOptions<BarbeariaSaasDbContext> _options;

        public TrialExpirationServiceTests()
        {
            var connectionString =
                Environment.GetEnvironmentVariable(
                    "BARBERMANAGER_TEST_CONNECTION")
                ?? throw new InvalidOperationException(
                    "Configure BARBERMANAGER_TEST_CONNECTION " +
                    "com uma base SQL Server exclusiva para testes.");

            _options =
                new DbContextOptionsBuilder<BarbeariaSaasDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;
        }

        [Fact]
        public async Task ExecucoesConcorrentes_ExpiramUmaUnicaVez()
        {
            var fimTrial = DateTime.UtcNow.AddMinutes(-5);
            var ids = await CriarCenarioAsync(fimTrial);

            try
            {
                var agoraUtc = DateTime.UtcNow;

                var tarefa1 = ProcessarEmNovoContextoAsync(agoraUtc);
                var tarefa2 = ProcessarEmNovoContextoAsync(agoraUtc);

                var resultados = await Task.WhenAll(tarefa1, tarefa2);

                Assert.Equal(1, resultados.Sum());

                await using var db = CriarContexto();

                var assinatura = await db.Assinaturas
                    .AsNoTracking()
                    .SingleAsync(x => x.Id == ids.AssinaturaId);

                Assert.Equal(
                    StatusAssinaturaCliente.Expirada,
                    assinatura.Ativa);
            }
            finally
            {
                await LimparCenarioAsync(ids);
            }
        }

        [Fact]
        public async Task ExecucaoPosterior_ARecuperaTrialVencido()
        {
            var fimTrial = DateTime.UtcNow.AddMinutes(5);
            var ids = await CriarCenarioAsync(fimTrial);

            try
            {
                var antesDoVencimento = fimTrial.AddMinutes(-1);

                var primeiraExecucao =
                    await ProcessarEmNovoContextoAsync(
                        antesDoVencimento);

                Assert.Equal(0, primeiraExecucao);

                // Simula uma execução posterior à indisponibilidade
                // ou ao atraso do processamento periódico.
                var depoisDoVencimento = fimTrial.AddMinutes(1);

                var segundaExecucao =
                    await ProcessarEmNovoContextoAsync(
                        depoisDoVencimento);

                Assert.Equal(1, segundaExecucao);

                // Reprocessar o mesmo período não altera novamente
                // uma assinatura já expirada.
                var terceiraExecucao =
                    await ProcessarEmNovoContextoAsync(
                        depoisDoVencimento.AddMinutes(1));

                Assert.Equal(0, terceiraExecucao);

                await using var db = CriarContexto();

                var assinatura = await db.Assinaturas
                    .AsNoTracking()
                    .SingleAsync(x => x.Id == ids.AssinaturaId);

                Assert.Equal(
                    StatusAssinaturaCliente.Expirada,
                    assinatura.Ativa);
            }
            finally
            {
                await LimparCenarioAsync(ids);
            }
        }

        private async Task<int> ProcessarEmNovoContextoAsync(
            DateTime agoraUtc)
        {
            await using var db = CriarContexto();

            ITrialExpirationService servico =
                new TrialExpirationService(db);

            return await servico.ExpirarTrialsVencidosAsync(
                agoraUtc,
                CancellationToken.None);
        }

        private async Task<(Guid PlanoId, Guid ResponsavelId,
            Guid EmpresaId, Guid AssinaturaId)> CriarCenarioAsync(
            DateTime fimTrial)
        {
            await using var db = CriarContexto();

            var agora = DateTime.UtcNow;

            var plano = new Plano
            {
                Nome = $"Plano de teste {Guid.NewGuid():N}",
                ValorMensal = 0m,
                ValorAnual = 0m,
                LimiteProfissionais = 2,
                LimiteEmpresas = 1,
                Ativo = StatusGeral.Ativo,
                EhTrial = true,
                DuracaoTrialDias = 30
            };

            var responsavel = new Responsavel
            {
                Nome = "Responsável de teste",
                Email = $"{Guid.NewGuid():N}@example.test",
                Telefone = "11999999999",
                UsuarioId = Guid.NewGuid(),
                Empresas = new List<Empresa>()
            };

            var empresa = new Empresa
            {
                NomeFantasia = $"Empresa teste {Guid.NewGuid():N}",
                RazaoSocial = "Empresa de teste",
                Email = responsavel.Email,
                Telefone = responsavel.Telefone,
                Ativa = StatusGeral.Ativo,
                Responsavel = responsavel,
                ResponsavelId = responsavel.Id
            };

            var assinatura = new Assinatura
            {
                Empresa = empresa,
                EmpresaId = empresa.Id,
                Plano = plano,
                PlanoId = plano.Id,
                DataInicio = agora.AddDays(-30),
                DataVencimento = fimTrial,
                DiaVencimento = fimTrial.Day,
                Valor = 0m,
                Trial = true,
                DataFimTrial = fimTrial,
                Ativa = StatusAssinaturaCliente.Ativa,
                RenovacaoAutomatica = false,
                PeriodicidadeCobranca =
                    PeriodicidadeCobranca.Mensal
            };

            db.Planos.Add(plano);
            db.Responsaveis.Add(responsavel);
            db.Empresas.Add(empresa);
            db.Assinaturas.Add(assinatura);

            await db.SaveChangesAsync();

            return (
                plano.Id,
                responsavel.Id,
                empresa.Id,
                assinatura.Id);
        }

        private async Task LimparCenarioAsync(
            (Guid PlanoId, Guid ResponsavelId,
             Guid EmpresaId, Guid AssinaturaId) ids)
        {
            await using var db = CriarContexto();

            await db.Assinaturas
                .Where(x => x.Id == ids.AssinaturaId)
                .ExecuteDeleteAsync();

            await db.Empresas
                .Where(x => x.Id == ids.EmpresaId)
                .ExecuteDeleteAsync();

            await db.Responsaveis
                .Where(x => x.Id == ids.ResponsavelId)
                .ExecuteDeleteAsync();

            await db.Planos
                .Where(x => x.Id == ids.PlanoId)
                .ExecuteDeleteAsync();
        }

        private BarbeariaSaasDbContext CriarContexto()
        {
            return new BarbeariaSaasDbContext(_options);
        }

        [Fact]
        public async Task Handle_Trial_NaoDeveCriarFatura()
        {
            var assinaturaId = Guid.NewGuid();

            var assinatura = new Assinatura
            {
                Id = assinaturaId,
                Trial = true,
                Ativa = StatusAssinaturaCliente.Ativa,
                Valor = 100m
            };

            var repositorio = new Mock<IRepositorioFatura>();

            repositorio
                .Setup(r => r.ObterAssinaturaAsync(
                    assinaturaId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(assinatura);

            var handler = new GerarFaturaHandler(repositorio.Object);

            var command = new GerarFaturaCommand(
                assinaturaId,
                new DateTime(2026, 11, 1),
                new DateTime(2026, 12, 1),
                new DateTime(2026, 11, 1));

            var resultado = await handler.Handle(
                command,
                CancellationToken.None);

            Assert.True(resultado.Ignorada);

            repositorio.Verify(
                r => r.AdicionarAsync(
                    It.IsAny<Fatura>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
