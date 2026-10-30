using Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Produtos
{
    public interface IRepositorioProdutos
    {
        Task<( IReadOnlyList<Produto> Itens, int TotalRegistros
    )> ListarAsync(
        Guid empresaId,int pageNumber,
        int pageSize, string? searchTerm,
        Guid? categoriaId, Guid? marcaId,
        bool? ativo,CancellationToken cancellationToken);

        Task<Produto?> ObterPorIdAsync(
    Guid empresaId, Guid produtoId,
    CancellationToken cancellationToken);

        Task<bool> CategoriaPertenceEmpresaAsync(
    Guid empresaId,
    Guid categoriaId,
    CancellationToken cancellationToken);

        Task<bool> MarcaPertenceEmpresaAsync(
            Guid empresaId, Guid marcaId,
            CancellationToken cancellationToken);

        Task<bool> CodigoInternoExisteAsync(
            Guid empresaId,string codigoInterno,
            CancellationToken cancellationToken);

        Task<bool> CodigoBarrasExisteAsync(
            Guid empresaId,
            string codigoBarras,
            CancellationToken cancellationToken);

       Task Adicionar(Produto produto);
    }
}

