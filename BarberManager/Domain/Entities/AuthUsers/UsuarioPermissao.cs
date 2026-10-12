namespace Domain.Entities.AuthUsers
{
    public class UsuarioPermissao
    {
        public Guid UsuarioId { get; set; }

        public Guid PermissaoId { get; set; }

        public Usuario Usuario { get; set; } = null!;
        public PermissaoGrupo Permissao { get; set; } = null!;
    }
}