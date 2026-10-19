using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Empresas.DTOs
{
    public class CriarEmpresaDto
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string NomeResponsavel { get; init; } = null!;

        [Required]
        [Phone]
        [StringLength(20)]
        public string Telefone { get; init; } = null!;

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string NomeBarbearia { get; init; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; init; } = null!;

        [Required]
        [StringLength(128, MinimumLength = 8)]
        public string Senha { get; init; } = null!;

        [Required]
        public Guid PlanoId { get; init; }

        [Required]
        public PeriodicidadeCobranca PeriodicidadeCobranca { get; init; }
    }
}
