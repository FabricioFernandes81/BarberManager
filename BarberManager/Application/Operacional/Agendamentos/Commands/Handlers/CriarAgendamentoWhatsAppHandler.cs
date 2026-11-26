using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Interfaces.Agendamentos;
using Application.Interfaces.Integracoes.WhatsApp;
using Application.Operacional.Agendamentos.Queries;

namespace Application.Operacional.Agendamentos.Commands.Handlers;

public sealed class CriarAgendamentoWhatsAppHandler
    : ICommandHandler<CriarAgendamentoWhatsAppCommand, CriarAgendamentoDto>
{
    private readonly IClienteWhatsAppResolver _clienteResolver;
    private readonly ICriarAgendamentoService _criarAgendamentoService;

    public CriarAgendamentoWhatsAppHandler(
        IClienteWhatsAppResolver clienteResolver,
        ICriarAgendamentoService criarAgendamentoService)
    {
        _clienteResolver = clienteResolver;
        _criarAgendamentoService = criarAgendamentoService;
    }

    public async Task<CriarAgendamentoDto> Handle(
        CriarAgendamentoWhatsAppCommand command,
        CancellationToken cancellationToken)
    {
        // 1. Validar dados recebidos.

        if (string.IsNullOrWhiteSpace(command.InstanceName))
            throw Erro("A instância WhatsApp é obrigatória.");

        if (string.IsNullOrWhiteSpace(command.Telefone))
            throw Erro("O telefone do cliente é obrigatório.");

        if (command.ProfissionalId == Guid.Empty ||
            command.ServicoId == Guid.Empty)
        {
            throw Erro(
                "Profissional e serviço são obrigatórios.");
        }

        // 2. Resolver empresa e cliente pela instância e telefone.

        var resolucao = await _clienteResolver.ResolverAsync(
            command.InstanceName,
            command.Telefone,
            cancellationToken);

        if (resolucao is null)
        {
            throw Erro(
                "Não foi encontrado um cliente ativo vinculado a esta barbearia.");
        }

        // 3. Criar o comando tradicional.

        var criarAgendamentoCommand = new CriarAgendamentoCommand(
            ClienteId: resolucao.ClienteId,
            ProfissionalId: command.ProfissionalId,
            ServicoId: command.ServicoId,
            DataHoraInicio: command.DataHoraInicio,
            Observacoes: command.Observacoes
        );

        // 4. Reutilizar o serviço compartilhado.

        return await _criarAgendamentoService.CriarAsync(
            resolucao.EmpresaId,
            criarAgendamentoCommand,
            cancellationToken);
    }

    private static CustomValidationException Erro(string mensagem)
        => new(new[] { mensagem });
}