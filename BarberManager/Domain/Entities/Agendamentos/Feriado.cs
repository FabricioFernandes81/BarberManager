using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Agendamentos
{
    public class Feriado
    {
        public Guid Id { get; private set; }

        public Guid EmpresaId { get; private set; }

        public DateOnly Data { get; private set; }

        public string Nome { get; private set; } = string.Empty;

        public bool Ativo { get; private set; }

        protected Feriado()
        {
        }

        public Feriado(
            Guid empresaId,
            DateOnly data,
            string nome)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException(
                    "A empresa deve ser informada.", nameof(empresaId));

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome do feriado deve ser informado.", nameof(nome));

            if (nome.Length > 150)
                throw new ArgumentException(
                    "O nome do feriado deve ter no máximo 150 caracteres.",
                    nameof(nome));

            Id = Guid.NewGuid();
            EmpresaId = empresaId;
            Data = data;
            Nome = nome.Trim();
            Ativo = true;
        }

        public void Atualizar(string nome, DateOnly data)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome do feriado deve ser informado.", nameof(nome));

            if (nome.Length > 150)
                throw new ArgumentException(
                    "O nome do feriado deve ter no máximo 150 caracteres.",
                    nameof(nome));

            Nome = nome.Trim();
            Data = data;
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;
    }
}
