using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Compras;
using Application.Operacional.Compras.DTOs;
using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Compras.Commands.Handlers
{
    public sealed class CancelarCompraHandler
        : ICommandHandler<
            CancelarCompraCommand,
            CompraCanceladaDto>
    {
        private readonly IRepositorioCompra _repositorioCompra;
        private readonly ITenantContext _tenantContext;

        public CancelarCompraHandler(
            IRepositorioCompra repositorioCompra,
            ITenantContext tenantContext)
        {
            _repositorioCompra = repositorioCompra;
            _tenantContext = tenantContext;
        }

        public async Task<CompraCanceladaDto> Handle(
            CancelarCompraCommand command,
            CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            // 1. Validar empresa autenticada.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário autenticado.");
            }

            // 2. Validar identificador.
            if (command.CompraId == Guid.Empty)
            {
                erros.Add("O identificador da compra é inválido.");
                throw new CustomValidationException(erros);
            }

            cancellationToken.ThrowIfCancellationRequested();

            var empresaId = _tenantContext.EmpresaId;

            // 3. Obter compra rastreada dentro da empresa.
            var compra =
                await _repositorioCompra.ObterParaCancelamentoAsync(
                    empresaId,
                    command.CompraId,
                    cancellationToken);

            if (compra is null)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Compra não encontrada nesta empresa."
                    });
            }

            // 4. Validar situação da compra.
            if (compra.Status == StatusCompra.Cancelada)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Esta compra já está cancelada."
                    });
            }

            if (compra.Status != StatusCompra.Pendente)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Somente compras pendentes e sem recebimento podem ser canceladas."
                    });
            }

            if (compra.Recebimentos.Any())
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Não é possível cancelar uma compra que possui histórico de recebimento."
                    });
            }

            if (compra.Itens.Any(i => i.QuantidadeRecebida > 0))
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Não é possível cancelar uma compra que possui itens recebidos."
                    });
            }

            cancellationToken.ThrowIfCancellationRequested();

            // 5. Obter contas a pagar vinculadas à compra.
            var contasPagar =
                await _repositorioCompra
                    .ObterContasPagarParaCancelamentoAsync(
                        empresaId,
                        compra.Id,
                        cancellationToken);

            // 6. Validar todas as contas antes de alterar qualquer entidade.
            foreach (var conta in contasPagar)
            {
                if (conta.Status == StatusContas.Cancelada)
                {
                    erros.Add(
                        $"A conta a pagar da parcela {conta.NumeroParcela} já está cancelada.");

                    continue;
                }

                if (conta.Status == StatusContas.Parcial ||
                    conta.Status == StatusContas.Recebida ||
                    conta.ValorPago > 0 ||
                    conta.Pagamentos.Any())
                {
                    erros.Add(
                        $"A conta a pagar da parcela {conta.NumeroParcela} possui pagamento registrado e impede o cancelamento da compra.");

                    continue;
                }

                if (conta.Status != StatusContas.Aberta &&
                    conta.Status != StatusContas.Vencida)
                {
                    erros.Add(
                        $"A conta a pagar da parcela {conta.NumeroParcela} não permite cancelamento.");
                }
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 7. Executar as alterações de domínio.
            try
            {
                compra.Cancelar();

                foreach (var conta in contasPagar)
                    conta.Cancelar();
            }
            catch (InvalidOperationException ex)
            {
                throw new CustomValidationException(
                    new[] { ex.Message });
            }

            // 8. O TransactionMiddleware persiste as entidades rastreadas.
            return new CompraCanceladaDto(
                CompraId: compra.Id,
                Status: compra.Status,
                DataAtualizacao: compra.DataAtualizacao,
                ContasPagarCanceladas: contasPagar.Count);
        }
    }
}