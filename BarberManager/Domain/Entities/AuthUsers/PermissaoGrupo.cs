using Domain.Entities.Common;

namespace Domain.Entities.AuthUsers
{
    public class PermissaoGrupo : EntityBase
    {
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;

        public ICollection<GrupoPermissaoPermissao> GrupoPermissao { get; set; }
    }
}