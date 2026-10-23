using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Profissionais;
using Application.Operacional.Profissionais.DTOs;
using Domain.Entities.Agendamentos;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Profissionais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Profissionais.Commands.Handlers
{
    public class CriarProfissionalHandler : ICommandHandler<CriarProfissionalCommand, CriarProfissionalDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioProfissionais _repositorio;

        public CriarProfissionalHandler(
        ITenantContext tenantContext,  IRepositorioProfissionais repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<CriarProfissionalDto> Handle(CriarProfissionalCommand command, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            if(!_tenantContext.IsAvailable || _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("Não foi possível identificar a empresa do usuário.");
            }
            var nome = command.Nome?.Trim() ?? string.Empty;
            var cpf = ApenasDigitos(command.Cpf);
            var celular = command.Celular?.Trim() ?? string.Empty;
            var email = command.Email?.Trim() ?? string.Empty;
            var matricula = command.Matricula?.Trim() ?? string.Empty;
            var cargo = command.Cargo?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(nome))
                erros.Add("O nome do profissional é obrigatório.");

            if (!CpfValido(cpf))
                erros.Add("O CPF informado é inválido.");

            if (string.IsNullOrWhiteSpace(celular))
                erros.Add("O celular é obrigatório.");

            if (!EmailValido(email))
                erros.Add("O e-mail informado é inválido.");

            if (string.IsNullOrWhiteSpace(matricula))
                erros.Add("A matrícula é obrigatória.");

            if (string.IsNullOrWhiteSpace(cargo))
                erros.Add("O cargo é obrigatório.");

            if (command.DataAdmissao == default)
                erros.Add("A data de admissão é obrigatória.");

            if (command.DataNascimento.HasValue &&
                command.DataNascimento.Value.Date > DateTime.Today)
            {
                erros.Add("A data de nascimento não pode estar no futuro.");
            }

            if (command.IntervaloAtendimento < 0)
                erros.Add("O intervalo de atendimento não pode ser negativo.");

            if (command.ValorTipoPagamento < 0)
                erros.Add("O valor de comissão não pode ser negativo.");

            if (command.TipoComissao is
                    TipoPagamento.PercentualServico or
                    TipoPagamento.PercentualVenda or
                    TipoPagamento.PercentualGeral &&
                command.ValorTipoPagamento > 100)
            {
                erros.Add("A comissão percentual deve estar entre 0 e 100.");
            }

            if (!Enum.IsDefined(typeof(EnumSexo), command.Sexo))
                erros.Add("O sexo informado é inválido.");

            if (!Enum.IsDefined(typeof(TipoPagamento), command.TipoComissao))
                erros.Add("O tipo de comissão é inválido.");

            if (!Enum.IsDefined(typeof(StatusProfissional), command.Status))
                erros.Add("O status do profissional é inválido.");

            var horarios = command.Horarios?.ToArray()
                ?? Array.Empty<HorarioProfissionalInput>();

            ValidarHorarios(horarios, erros);

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            var empresaId = _tenantContext.EmpresaId;

            if (await _repositorio.ExisteCpfAsync(
                empresaId, cpf, cancellationToken))
            {
                erros.Add("Já existe um profissional com esse CPF nesta empresa.");
            }

            if (await _repositorio.ExisteMatriculaAsync(
                empresaId, matricula, cancellationToken))
            {
                erros.Add("Já existe um profissional com essa matrícula nesta empresa.");
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            var profissional = new Profissional
            {
                EmpresaId = empresaId,
                Nome = nome,
                Sobrenome = command.Sobrenome?.Trim(),
                DataNascimento = command.DataNascimento,
                Sexo = command.Sexo,
                Cpf = cpf,
                Rg = command.Rg?.Trim(),
                Celular = celular,
                Telefone = command.Telefone?.Trim(),
                Email = email,
                Matricula = matricula,
                Cargo = cargo,
                DataAdmissao = command.DataAdmissao,
                TipoComissao = command.TipoComissao,
                ValorTipoPagamento = command.ValorTipoPagamento,
                IntervaloAtendimento = command.IntervaloAtendimento,
                PermiteEncaixe = command.PermiteEncaixe,
                AgendaOnline = command.AgendaOnline,
                Biografia = command.Biografia?.Trim(),
                Foto = command.Foto?.Trim(),
                Status = command.Status,
                ExibirAgendamentoOnline = command.ExibirAgendamentoOnline,
                RecebePagamentoDireto = command.RecebePagamentoDireto,
                OrdemAgenda = command.OrdemAgenda,
                Instagram = command.Instagram?.Trim(),
                Facebook = command.Facebook?.Trim(),
                Especialidade = command.Especialidade?.Trim(),
                Destaque = command.Destaque
            };

            var horariosEntidade = horarios
                .Select(h => new HorarioProfissional(
                    empresaId,
                    profissional.Id,
                    h.DiaSemana,
                    h.HoraInicio,
                    h.HoraFim))
                .ToArray();

            await _repositorio.CriarAsync(
                profissional,
                horariosEntidade,
                cancellationToken);

            return new CriarProfissionalDto(
                profissional.Id,
                profissional.Nome,
                profissional.Cpf,
                profissional.Matricula,
                profissional.Status.ToString(),
                horariosEntidade.Length);
        }

        private static void ValidarHorarios(
        IReadOnlyCollection<HorarioProfissionalInput> horarios,
        List<string> erros)
        {
            foreach (var horario in horarios)
            {
                if (!Enum.IsDefined(typeof(DayOfWeek), horario.DiaSemana))
                    erros.Add("Um dos dias da semana informados é inválido.");

                if (horario.HoraFim <= horario.HoraInicio)
                    erros.Add(
                        $"O horário final deve ser posterior ao inicial em {horario.DiaSemana}.");
            }

            foreach (var grupo in horarios.GroupBy(h => h.DiaSemana))
            {
                var ordenados = grupo
                    .OrderBy(h => h.HoraInicio)
                    .ToArray();

                for (var i = 1; i < ordenados.Length; i++)
                {
                    if (ordenados[i].HoraInicio < ordenados[i - 1].HoraFim)
                    {
                        erros.Add(
                            $"Existem horários sobrepostos em {grupo.Key}.");
                        break;
                    }
                }
            }
        }

        private static string ApenasDigitos(string? valor) =>
            new string((valor ?? string.Empty)
                .Where(char.IsDigit)
                .ToArray());

        private static bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var endereco = new MailAddress(email);
                return endereco.Address.Equals(
                    email,
                    StringComparison.OrdinalIgnoreCase);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static bool CpfValido(string cpf)
        {
            if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
                return false;

            static int Digito(string valor, int quantidade)
            {
                var soma = 0;

                for (var i = 0; i < quantidade; i++)
                    soma += (valor[i] - '0') * (quantidade + 1 - i);

                var resto = soma % 11;
                return resto < 2 ? 0 : 11 - resto;
            }

            var primeiro = Digito(cpf, 9);
            if (primeiro != cpf[9] - '0')
                return false;

            var segundo = Digito(cpf, 10);
            return segundo == cpf[10] - '0';
        }
    }
}
