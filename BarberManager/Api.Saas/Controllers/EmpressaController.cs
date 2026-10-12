using Application.Common.Pagination;
using Application.Dispatcher;
using Application.Empresas.DTOs;
using Application.Empresas.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                (new GetEmpresaQuery());

            return Ok(empresas);
        }

    }
}
