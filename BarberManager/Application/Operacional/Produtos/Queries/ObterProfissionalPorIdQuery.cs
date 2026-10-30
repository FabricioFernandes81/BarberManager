using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Produtos.Queries
{
    public sealed record ObterProdutoPorIdQuery(Guid ProdutoId);
}
