using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Middlewares
{
    public interface IValidator<T>
    {
        Task<ValidationResult> ValidateAsync(T request);
    }

}
