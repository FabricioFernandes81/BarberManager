using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create(TransactionContext context);
    }
}
