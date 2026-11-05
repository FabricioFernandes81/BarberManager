using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class IdempotencyConflictException : Exception
    {
        public IdempotencyConflictException(string message)
            : base(message)
        {
        }
    }
}
