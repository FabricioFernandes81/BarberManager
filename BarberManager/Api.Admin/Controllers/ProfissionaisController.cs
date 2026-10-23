using Application.Dispatcher;
using Application.Operacional.Profissionais.Commands;
using Application.Operacional.Profissionais.DTOs;
using Application.Operacional.Profissionais.Queries;
using Domain.Entities.Common.Enuns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfissionaisController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ProfissionaisController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [ProducesResponseType(
        typeof(ListarProfissionaisDto),
        StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ListarProfissionaisDto>> Listar(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] StatusProfissional? status = null,
        CancellationToken cancellationToken = default)
        {
            var query = new ListarProfissionaisQuery(
                pageNumber,
                pageSize,
                searchTerm,
                status);

            var resultado =
                await _dispatcher.Query<
                    ListarProfissionaisQuery,
                    ListarProfissionaisDto>(
                        query,
                        cancellationToken);

            return Ok(resultado);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CriarProfissionalDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CriarProfissionalDto>> Criar(
    [FromBody] CriarProfissionalCommandDto command,
    CancellationToken cancellationToken)
        {
            var resultado =
                await _dispatcher.Command<
                    CriarProfissionalCommand,
                    CriarProfissionalDto>(
                    new CriarProfissionalCommand
                    {
                        Nome = command.Nome,
                        Cpf = command.Cpf,
                        Celular = command.Celular,
                        Email = command.Email,
                        Matricula = command.Matricula,
                        Cargo = command.Cargo,
                        DataAdmissao = command.DataAdmissao,
                        Horarios = command.Horarios?
                     .Select(h => new HorarioProfissionalInput
                     {
                         DiaSemana = h.DiaSemana,
                         HoraInicio = h.HoraInicio,
                         HoraFim = h.HoraFim
                     }).ToList(),
                        Sobrenome = command.Sobrenome,
                        DataNascimento = command.DataNascimento,
                        Sexo = command.Sexo,
                        Rg = command.Rg,
                        Telefone = command.Telefone,
                        TipoComissao = command.TipoComissao,
                        ValorTipoPagamento = command.ValorTipoPagamento,
                        IntervaloAtendimento = command.IntervaloAtendimento,
                        PermiteEncaixe = command.PermiteEncaixe,
                        AgendaOnline = command.AgendaOnline,
                        Biografia = command.Biografia,
                        Foto = command.Foto,
                        Status = command.Status,
                        ExibirAgendamentoOnline =
                    command.ExibirAgendamentoOnline,
                        RecebePagamentoDireto = command.RecebePagamentoDireto,
                        OrdemAgenda = command.OrdemAgenda,
                        Instagram = command.Instagram,
                        Facebook = command.Facebook,
                        Especialidade = command.Especialidade,
                        Destaque = command.Destaque
                    }, cancellationToken);

            return StatusCode(
                StatusCodes.Status201Created,
                resultado);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(
    typeof(ProfissionalDetalheDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProfissionalDetalheDto>> ObterPorId(
    [FromRoute] Guid id,
    CancellationToken cancellationToken)
        {
            var query = new ObterProfissionalPorIdQuery { ProfissionalId = id };

            var resultado =
                await _dispatcher.Query<
                    ObterProfissionalPorIdQuery,
                    ProfissionalDetalheDto>(
                        query,
                        cancellationToken);

            return Ok(resultado);
        }

    

    [HttpPut("{id:guid}")]
        [ProducesResponseType(
    typeof(AtualizarProfissionalDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AtualizarProfissionalDto>> Atualizar(
    [FromRoute] Guid id,
    [FromBody] AtualizarProfissionalCommand command,
    CancellationToken cancellationToken)
        {
            var comando = command with
            {
                ProfissionalId = id
            };

            var resultado = await _dispatcher.Command<
                AtualizarProfissionalCommand,
                AtualizarProfissionalDto>(
                    comando,
                    cancellationToken);

            return Ok(resultado);
        }
    }
}
