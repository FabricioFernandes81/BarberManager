
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Tenancy;

public interface ITenantContext
{
    Guid EmpresaId { get; }

    bool IsAvailable { get; }

}
