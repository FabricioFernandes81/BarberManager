using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Clientes.DTOs
{
    public sealed record CriarPerfilClienteResultadoDto(
    bool Sucesso,
    string Codigo,
    string Mensagem,
    PerfilClienteDto? Perfil = null);
}
