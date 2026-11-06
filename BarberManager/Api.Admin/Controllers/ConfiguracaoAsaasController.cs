using Application.Dispatcher;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.DTOs;
using Application.Operacional.Financeiro.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConfiguracaoAsaasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ConfiguracaoAsaasController(
            IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [ProducesResponseType(
            typeof(ConsultarConfiguracaoAsaasDto),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ConsultarConfiguracaoAsaasDto>> Consultar(
            CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Query<
                ConsultarConfiguracaoAsaasQuery,
                ConsultarConfiguracaoAsaasDto>(
                    new ConsultarConfiguracaoAsaasQuery(),
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpPut]
        [Authorize(Roles = "AdministradorBarber")]
        [ProducesResponseType(
    typeof(ConfiguracaoAsaasDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ConfiguracaoAsaasDto>> Configurar(
    [FromBody] ConfigurarCredenciaisAsaasRequest request,
    [FromServices] IConfiguracaoAsaasService configuracaoService,
    CancellationToken cancellationToken)
        {
            var resultado = await configuracaoService.ConfigurarAsync(
                request,
                cancellationToken);

            return Ok(resultado);
        }
    }
}