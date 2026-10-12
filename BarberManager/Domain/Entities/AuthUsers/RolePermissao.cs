namespace Domain.Entities.AuthUsers
{
    public class RolePermissao
    {
        public Guid RoleId { get; set; }

        public Guid PermissaoGupoId { get; set; }
        public Roles Role { get; set; } = null!;

        public PermissaoGrupo PermissaoGrupo { get; set; } = null!;
        
    }
}