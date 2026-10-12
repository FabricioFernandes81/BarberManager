using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Pagination
{
    public class Response<T>
    {
        public Response()
        {

        }
        public Response(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
