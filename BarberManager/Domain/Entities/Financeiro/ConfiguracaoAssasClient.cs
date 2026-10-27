using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Financeiro
{
    public class ConfiguracaoAssasClient : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public string ApiToken { get; set; }

        public string WebhooksToken { get; set; }

    }
}
