using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Profissionais;
using Application.Operacional.Profissionais.DTOs;
using Domain.Entities.Agendamentos;
using Domain.Entities.Common.Enuns;
using System.Net.Mail;

namespace Application.Operacional.Profissionais.Commands.Handlers
{
    public sealed class AtualizarProfissionalHandler
        : ICommandHandler<AtualizarProfissionalCommand, AtualizarProfissionalDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioProfissionais _repositorio;

        public AtualizarProfissionalHandler(
            ITenantContext tenantContext,
            IRepositorioProfissionais repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<AtualizarProfissionalDto> Handle(
            AtualizarProfissionalCommand command,
            CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            // 1. Validar o contexto da empresa.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            if (command.ProfissionalId == Guid.Empty)
            {
                erros.Add("O profissional informado é inválido.");
            }

            // 2. Normalizar os dados recebidos.
            var nome = command.Nome?.Trim() ?? string.Empty;
            var cpf = ApenasDigitos(command.Cpf);
            var celular = command.Celular?.Trim() ?? string.Empty;
            var email = command.Email?.Trim() ?? string.Empty;
            var matricula = command.Matricula?.Trim() ?? string.Empty;
            var cargo = command.Cargo?.Trim() ?? string.Empty;

            // 3. Validar os dados cadastrais.
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
                erros.Add(
                    "A data de nascimento não pode estar no futuro.");
            }

            if (command.DataDemissao.HasValue &&
                command.DataDemissao.Value.Date <
                command.DataAdmissao.Date)
            {
                erros.Add(
                    "A data de demissão não pode ser anterior à admissão.");
            }

            if (command.IntervaloAtendimento < 0)
                erros.Add(
                    "O intervalo de atendimento não pode ser negativo.");

            if (command.ValorTipoPagamento < 0)
                erros.Add(
                    "O valor de comissão não pode ser negativo.");

            if (command.TipoComissao is
                    TipoPagamento.PercentualServico or
                    TipoPagamento.PercentualVenda or
                    TipoPagamento.PercentualGeral &&
                command.ValorTipoPagamento > 100)
            {
                erros.Add(
                    "A comissão percentual deve estar entre 0 e 100.");
            }

            if (!Enum.IsDefined(typeof(EnumSexo), command.Sexo))
                erros.Add("O sexo informado é inválido.");

            if (!Enum.IsDefined(
                typeof(TipoPagamento), command.TipoComissao))
            {
                erros.Add("O tipo de comissão é inválido.");
            }

            if (!Enum.IsDefined(
                typeof(StatusProfissional), command.Status))
            {
                erros.Add("O status do profissional é inválido.");
            }

            // 4. Validar horários antes de consultar o banco.
            var horarios = command.Horarios?.ToArray()
                ?? Array.Empty<HorarioProfissionalAtualizacaoInput>();

            ValidarHorarios(horarios, erros);

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            var empresaId = _tenantContext.EmpresaId;

            // 5. Consultar o profissional com rastreamento.
            // O método deve garantir EmpresaId + ProfissionalId.
            var profissional = await _repositorio
                .ObterPorIdAsync(
                    empresaId,
                    command.ProfissionalId,
                    cancellationToken,
                    rastrearAlteracoes: true);

            if (profissional is null)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Profissional não encontrado nesta empresa."
                    });
            }

            // 6. Verificar duplicidades, ignorando o próprio registro.
            if (await _repositorio.ExisteCpfAsync(
                empresaId,
                cpf,
                cancellationToken,
                profissional.Id))
            {
                erros.Add(
                    "Já existe outro profissional com esse CPF nesta empresa.");
            }

            if (await _repositorio.ExisteMatriculaAsync(
                empresaId,
                matricula,
                cancellationToken,
                profissional.Id))
            {
                erros.Add(
                    "Já existe outro profissional com essa matrícula nesta empresa.");
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            // 7. Carregar horários existentes com rastreamento.
            var horariosExistentes = await _repositorio
                .ObterHorariosPorProfissionalAsync(
                    empresaId,
                    profissional.Id,
                    cancellationToken,
                    rastrearAlteracoes: true);

            var horariosPorId = horariosExistentes
                .ToDictionary(h => h.Id);

            // 8. Validar os IDs dos horários enviados.
            var idsInformados = horarios
                .Where(h => h.Id.HasValue)
                .Select(h => h.Id!.Value)
                .ToArray();

            if (idsInformados.Distinct().Count() != idsInformados.Length)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "A lista contém horários duplicados."
                    });
            }

            foreach (var horario in horarios.Where(h => h.Id.HasValue))
            {
                if (!horariosPorId.ContainsKey(horario.Id!.Value))
                {
                    throw new CustomValidationException(
                        new[]
                        {
                            "Um dos horários informados não pertence ao profissional."
                        });
                }
            }

            // 9. Atualizar os dados cadastrais.
            profissional.Nome = nome;
            profissional.Sobrenome = command.Sobrenome?.Trim();
            profissional.DataNascimento = command.DataNascimento;
            profissional.Sexo = command.Sexo;
            profissional.Cpf = cpf;
            profissional.Rg = command.Rg?.Trim();
            profissional.Celular = celular;
            profissional.Telefone = command.Telefone?.Trim();
            profissional.Email = email;
            profissional.Matricula = matricula;
            profissional.Cargo = cargo;
            profissional.DataAdmissao = command.DataAdmissao;
            profissional.DataDemissao = command.DataDemissao;
            profissional.TipoComissao = command.TipoComissao;
            profissional.ValorTipoPagamento = command.ValorTipoPagamento;
            profissional.IntervaloAtendimento = command.IntervaloAtendimento;
            profissional.PermiteEncaixe = command.PermiteEncaixe;
            profissional.AgendaOnline = command.AgendaOnline;
            profissional.Biografia = command.Biografia?.Trim();
            profissional.Foto = command.Foto?.Trim();
            profissional.Status = command.Status;
            profissional.ExibirAgendamentoOnline =
                command.ExibirAgendamentoOnline;
            profissional.RecebePagamentoDireto =
                command.RecebePagamentoDireto;
            profissional.OrdemAgenda = command.OrdemAgenda;
            profissional.Instagram = command.Instagram?.Trim();
            profissional.Facebook = command.Facebook?.Trim();
            profissional.Especialidade = command.Especialidade?.Trim();
            profissional.Destaque = command.Destaque;
            profissional.DataAtualizacao = DateTime.UtcNow;

            // 10. Sincronizar os horários semanais.
            var idsMantidos = new HashSet<Guid>();

            foreach (var horarioInput in horarios)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (horarioInput.Id.HasValue)
                {
                    var horarioExistente =
                        horariosPorId[horarioInput.Id.Value];

                    horarioExistente.AtualizarDiaEHorario(
                        horarioInput.DiaSemana,
                        horarioInput.HoraInicio,
                        horarioInput.HoraFim);

                    horarioExistente.Ativar();

                    idsMantidos.Add(horarioExistente.Id);
                }
                else
                {
                    var novoHorario = new HorarioProfissional(
                        empresaId,
                        profissional.Id,
                        horarioInput.DiaSemana,
                        horarioInput.HoraInicio,
                        horarioInput.HoraFim);

                    await _repositorio.AdicionarHorarioAsync(
                        novoHorario,
                        cancellationToken);

                    idsMantidos.Add(novoHorario.Id);
                }
            }

            // Horários omitidos da lista são desativados.
            foreach (var horarioExistente in horariosExistentes)
            {
                if (!idsMantidos.Contains(horarioExistente.Id))
                    horarioExistente.Desativar();
            }

            // 11. O TransactionMiddleware fará a persistência.
            return new AtualizarProfissionalDto(
                profissional.Id,
                profissional.Nome,
                profissional.Cpf,
                profissional.Matricula,
                profissional.Status.ToString(),
                idsMantidos.Count);
        }

        private static void ValidarHorarios(
            IReadOnlyCollection<HorarioProfissionalAtualizacaoInput> horarios,
            List<string> erros)
        {
            foreach (var horario in horarios)
            {
                if (!Enum.IsDefined(
                    typeof(DayOfWeek), horario.DiaSemana))
                {
                    erros.Add("Um dos dias da semana é inválido.");
                }

                if (horario.HoraFim <= horario.HoraInicio)
                {
                    erros.Add(
                        $"O horário final deve ser posterior ao inicial em {horario.DiaSemana}.");
                }
            }

            foreach (var grupo in horarios.GroupBy(h => h.DiaSemana))
            {
                var ordenados = grupo
                    .OrderBy(h => h.HoraInicio)
                    .ToArray();

                for (var i = 1; i < ordenados.Length; i++)
                {
                    if (ordenados[i].HoraInicio <
                        ordenados[i - 1].HoraFim)
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

            static int CalcularDigito(
                string valor,
                int quantidade)
            {
                var soma = 0;

                for (var i = 0; i < quantidade; i++)
                {
                    soma +=
                        (valor[i] - '0') * (quantidade + 1 - i);
                }

                var resto = soma % 11;

                return resto < 2 ? 0 : 11 - resto;
            }

            var primeiro = CalcularDigito(cpf, 9);

            if (primeiro != cpf[9] - '0')
                return false;

            var segundo = CalcularDigito(cpf, 10);

            return segundo == cpf[10] - '0';
        }
    }
}