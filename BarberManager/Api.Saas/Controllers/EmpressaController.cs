using Application.Common.Pagination;
using Application.Dispatcher;
using Application.Saas.Empresas.Commands;
using Application.Saas.Empresas.DTOs;
using Application.Saas.Empresas.Enums;
using Application.Saas.Empresas.Queries;
using Domain.Entities.Common.Enuns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Saas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpressaController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public EmpressaController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> ListarEmpresas([FromQuery] PaginationFilter filter)
        {

            var empresas = await _dispatcher.Query<GetEmpresaQuery, PagedResponse<List<GetListEmpresaDto>>>
                (new GetEmpresaQuery
                {
                    pageNumber = filter.PageNumber,
                    pageSize = filter.PageSize,
                    SearchTerm = filter.SearchTerm,
                    SortBy = filter.SortBy,
                    Status = filter.Status,
                    Route = Request.Path.Value
                });

            return Ok(empresas);
        }
        [HttpPatch("{empresaId:guid}/acesso")]
        public async Task<IActionResult> AlterarAcesso(
     Guid empresaId,
     [FromBody] AlterarAcessoEmpresaDto dto,
     CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<
                AlterarAcessoEmpresaCommand,
                ResultadoAlteracaoAcessoEmpresa>(
                    new AlterarAcessoEmpresaCommand
                    {
                        EmpresaId = empresaId,
                        HabilitarAcesso = dto.HabilitarAcesso,
                        Motivo = dto.Motivo
                    },
                    cancellationToken);

            return resultado switch
            {
                ResultadoAlteracaoAcessoEmpresa.Habilitado =>
                    Ok(new { mensagem = "Acesso da empresa habilitado." }),

                ResultadoAlteracaoAcessoEmpresa.Suspenso =>
                    Ok(new { mensagem = "Acesso da empresa suspenso." }),

                ResultadoAlteracaoAcessoEmpresa.JaHabilitado =>
                    Ok(new { mensagem = "O acesso da empresa já está habilitado." }),

                ResultadoAlteracaoAcessoEmpresa.JaSuspenso =>
                    Ok(new { mensagem = "O acesso da empresa já está suspenso." }),

                ResultadoAlteracaoAcessoEmpresa.EmpresaNaoEncontrada =>
                    NotFound(new { mensagem = "Empresa não encontrada." }),

                ResultadoAlteracaoAcessoEmpresa.EmpresaInativa =>
                    Conflict(new
                    {
                        mensagem =
                            "A empresa está inativa. Regularize seu estado cadastral antes de alterar o acesso."
                    }),

                _ => BadRequest(new
                {
                    mensagem =
                        "Dados inválidos. Para suspender o acesso, informe o motivo."
                })
            };
        }

        [AllowAnonymous]
        [EnableRateLimiting("cadastro-empresa")]
        [HttpPost]

        public async Task<ActionResult> CriarEmpresa(
            [FromBody] CriarEmpresaDto dto, CancellationToken cancellationToken)
        {
            var command = new CriarEmpresaCommand
            {
                NomeResponsavel = dto.NomeResponsavel,
                Telefone = dto.Telefone,
                NomeBarbearia = dto.NomeBarbearia,
                Email = dto.Email,
                Senha = dto.Senha,
                PlanoId = dto.PlanoId,
                PeriodicidadeCobranca = dto.PeriodicidadeCobranca
            };

            var result = await _dispatcher.Command<CriarEmpresaCommand, CriarEmpresaResultadoDto>(
                command, cancellationToken);

            if(!result.Sucesso)
            {
                return result.Codigo switch
                {
                    "DadosInvalidos" or "PeriodicidadeInvalida" =>
                   BadRequest(new { mensagem = result.Mensagem }),

                    "PlanoIndisponivel" =>
                        UnprocessableEntity(new { mensagem = result.Mensagem }),

                    _ =>
                        BadRequest(new { mensagem = result.Mensagem })

                };

            }
            return StatusCode(StatusCodes.Status201Created, new
            {
                result.EmpresaId,
                result.ResponsavelId,
                result.AssinaturaId,
                StatusAssinatura = StatusAssinaturaCliente.Pendente.ToString(),
                mesagem = result.Mensagem
            });
        }

    }
}
