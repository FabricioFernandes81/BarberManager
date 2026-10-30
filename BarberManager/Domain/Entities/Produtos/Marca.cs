using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Produtos
{
    public class Marca : EntityBase
    {
        public Guid EmpresaId { get;  set; }

        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public bool Ativo { get; set; }

        public ICollection<Produto> Produtos { get; set; }
            = new List<Produto>();

        public Marca()
        {
        }

        public Marca(
            Guid empresaId,
            string nome,
            string? descricao = null)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException(
                    "A empresa é obrigatória.",
                    nameof(empresaId));

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome da marca é obrigatório.",
                    nameof(nome));

            EmpresaId = empresaId;
            Nome = nome.Trim();
            Descricao = descricao?.Trim();
            Ativo = true;
        }

        public void Atualizar(
            string nome,
            string? descricao)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome da marca é obrigatório.",
                    nameof(nome));

            Nome = nome.Trim();
            Descricao = descricao?.Trim();
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Desativar()
        {
            Ativo = false;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Ativar()
        {
            Ativo = true;
            DataAtualizacao = DateTime.UtcNow;
        }

    }
}
