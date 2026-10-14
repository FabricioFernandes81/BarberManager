using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Transactions
{
    public interface ITransactionalCommand
    {
        TransactionContext TransactionContext { get; }
    }
}
