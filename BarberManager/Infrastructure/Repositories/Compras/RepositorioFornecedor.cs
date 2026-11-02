using Application.Interfaces.Compras;
using Domain.Entities.Compras;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Compras
{
    public sealed class RepositorioFornecedor : IRepositorioFornecedor
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioFornecedor(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<(
            IReadOnlyList<Fornecedor> Itens,
            int TotalRegistros
        )> ListarAsync(
            Guid empresaId,
            int pageNumber,
            int pageSize,
            string? searchTerm,
            bool? ativo,
            CancellationToken cancellationToken)
        {
            IQueryable<Fornecedor> consulta = _context.Fornecedores
                .AsNoTracking()
                .Where(f => f.EmpresaId == empresaId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var termo = searchTerm.Trim();

                consulta = consulta.Where(f =>
                    f.Nome.Contains(termo) ||
                    (f.NomeFantasia != null &&
                     f.NomeFantasia.Contains(termo)) ||
                    (f.Documento != null &&
                     f.Documento.Contains(termo)) ||
                    (f.Email != null &&
                     f.Email.Contains(termo)) ||
                    (f.Telefone != null &&
                     f.Telefone.Contains(termo)));
            }

            if (ativo.HasValue)
            {
                consulta = consulta.Where(
                    f => f.Ativo == ativo.Value);
            }

            var totalRegistros = await consulta.CountAsync(
                cancellationToken);

            var fornecedores = await consulta
                .OrderBy(f => f.Nome)
                .ThenBy(f => f.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (fornecedores, totalRegistros);
        }

        public async Task<Fornecedor?> ObterPorIdAsync(
            Guid empresaId,
            Guid fornecedorId,
            CancellationToken cancellationToken)
        {
            return await _context.Fornecedores
                .AsNoTracking()
                .SingleOrDefaultAsync(
                    f => f.Id == fornecedorId &&
                         f.EmpresaId == empresaId,
                    cancellationToken);
        }

        public async Task<Fornecedor?> ObterPorIdParaAtualizacaoAsync(
            Guid empresaId,
            Guid fornecedorId,
            CancellationToken cancellationToken)
        {
            return await _context.Fornecedores
                .SingleOrDefaultAsync(
                    f => f.Id == fornecedorId &&
                         f.EmpresaId == empresaId,
                    cancellationToken);
        }

        public async Task<bool> DocumentoExisteAsync(
            Guid empresaId,
            string documento,
            CancellationToken cancellationToken,
            Guid? ignorarFornecedorId = null)
        {
            return await _context.Fornecedores
                .AsNoTracking()
                .AnyAsync(
                    f => f.EmpresaId == empresaId &&
                         f.Documento == documento &&
                         (!ignorarFornecedorId.HasValue ||
                          f.Id != ignorarFornecedorId.Value),
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            Fornecedor fornecedor,
            CancellationToken cancellationToken)
        {
            await _context.Fornecedores.AddAsync(
                fornecedor,
                cancellationToken);
        }

        public Task AtualizarAsync(Fornecedor fornecedor)
        {
            // A entidade deve chegar rastreada pelo contexto,
            // através de ObterPorIdParaAtualizacaoAsync.
            // O EF Core detectará as alterações no SaveChanges
            // executado pela infraestrutura transacional.

            return Task.CompletedTask;
        }
    }
}
