using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.Commands
{
    public sealed record AtualizarFornecedorCommand(
       Guid FornecedorId,
       string Nome,
       string? NomeFantasia,
       string? Documento,
       string? Email,
       string? Telefone,
       string? Observacao
   ) : ITransactionalCommand
    {
        [NotMapped]
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
