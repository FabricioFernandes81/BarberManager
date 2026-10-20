using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Interfaces.Empresas;
using Application.Saas.Pagamentos.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.Commands.Handlers
{
    public class GerarFaturaHandler : ICommandHandler<GerarFaturaCommand, GerarFaturaDto>
    {
        private readonly IRepositorioFatura _repositorioFatura;

        public GerarFaturaHandler(IRepositorioFatura repositorioFatura)
        {
            _repositorioFatura = repositorioFatura;
        }

        public async Task<GerarFaturaDto> Handle(
         GerarFaturaCommand command,
         CancellationToken cancellationToken)
        {
            ValidarCommand(command);

            var periodoInicio = command.PeriodoInicio.Date;
            var periodoFim = command.PeriodoFim.Date;
            var vencimento = command.Vencimento.Date;

            // 1. Localizar a assinatura.
            var assinatura =
                await _repositorioFatura.ObterAssinaturaAsync(
                    command.AssinaturaId,
                    cancellationToken);

            if (assinatura is null)
            {
                throw ErroValidacao("Assinatura não encontrada.");
            }

            // 2. Trial nunca gera fatura.
            // O encerramento do Trial é tratado pelo processo
            // responsável pela expiração da assinatura.
            if (assinatura.Trial)
            {
                return new GerarFaturaDto
                {
                    AssinaturaId = assinatura.Id,
                    Ignorada = true,
                    Motivo = "Assinaturas em Trial não geram faturas."
                };
            }

            // 3. Não faturar assinaturas encerradas ou suspensas.
            if (assinatura.Ativa is StatusAssinaturaCliente.Cancelada
                or StatusAssinaturaCliente.Expirada
                or StatusAssinaturaCliente.Suspensa)
            {
                throw ErroValidacao(
                    "Não é possível gerar fatura para uma assinatura cancelada, expirada ou suspensa.");
            }

            // 4. Verificar se o período já foi faturado.
            var faturaExistente =
                await _repositorioFatura.ObterPorPeriodoAsync(
                    assinatura.Id,
                    periodoInicio,
                    periodoFim,
                    cancellationToken);

            if (faturaExistente is not null)
            {
                return MapearDto(
                    faturaExistente,
                    jaExistia: true);
            }


            if (assinatura.Valor < 0)
            {
                throw ErroValidacao(
                    "O valor da assinatura não pode ser negativo.");
            }

            if (assinatura.Valor == 0)
            {
                throw ErroValidacao(
                    "Não é possível gerar uma fatura para uma assinatura com valor zero.");
            }

            var valorFatura = assinatura.PeriodicidadeCobranca switch
            {
                PeriodicidadeCobranca.Mensal =>
                    assinatura.Valor,

                PeriodicidadeCobranca.Anual =>
                    assinatura.Valor * 12m,

                _ => throw ErroValidacao(
                    "Periodicidade de cobrança não suportada.")
            };

            if (valorFatura <= 0)
            {
                throw ErroValidacao(
                    "O valor da fatura deve ser maior que zero.");
            }

            // 5. Criar a fatura com o valor contratado.
            var fatura = new Fatura
            {
                AssinaturaId = assinatura.Id,
                Valor = valorFatura,
                PeriodoInicio = periodoInicio,
                PeriodoFim = periodoFim,
                Vencimento = vencimento,
                Status = StatusFatura.Pendente
            };

            // 6. Preparar a persistência.
            // A UnitOfWork executará SaveChanges e commit.
            await _repositorioFatura.AdicionarAsync(
                fatura,
                cancellationToken);

            return MapearDto(
                fatura,
                jaExistia: false);
        }

        private static void ValidarCommand(
            GerarFaturaCommand command)
        {
            if (command.AssinaturaId == Guid.Empty)
            {
                throw ErroValidacao(
                    "A identificação da assinatura é obrigatória.");
            }

            if (command.PeriodoInicio == default
                || command.PeriodoFim == default
                || command.Vencimento == default)
            {
                throw ErroValidacao(
                    "O período de cobrança e o vencimento são obrigatórios.");
            }

            if (command.PeriodoInicio.Date >= command.PeriodoFim.Date)
            {
                throw ErroValidacao(
                    "O início do período deve ser anterior ao fim do período.");
            }
        }

        private static CustomValidationException ErroValidacao(
            string mensagem)
        {
            return new CustomValidationException(
                new[] { mensagem });
        }

        private static GerarFaturaDto MapearDto(
            Fatura fatura,
            bool jaExistia)
        {
            return new GerarFaturaDto
            {
                FaturaId = fatura.Id,
                AssinaturaId = fatura.AssinaturaId,
                Valor = fatura.Valor,
                Vencimento = fatura.Vencimento,
                PeriodoInicio = fatura.PeriodoInicio,
                PeriodoFim = fatura.PeriodoFim,
                Status = fatura.Status.ToString(),
                JaExistia = jaExistia
            };
        }
    }
}
