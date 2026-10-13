using Application.Common.Pagination;
using Application.Dispatcher;
using Application.Saas.Assinaturas.DTOs;
using Application.Saas.Assinaturas.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Saas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssinaturaController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public AssinaturaController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        [HttpGet]

        public async Task<ActionResult> ListarAssinaturas([FromQuery] PaginationFilter filter)
        {
            var result = await _dispatcher.Query<GetAssinaturaQuery, PagedResponse<List<GetAssinaturaDto>>> (
                new GetAssinaturaQuery
                {
                    pageNumber = filter.PageNumber,
                    pageSize = filter.PageSize,
                    SearchTerm = filter.SearchTerm,
                    SortBy = filter.SortBy,
                    Status = filter.Status,
                    Route = Request.Path.Value
                });

            return Ok(result);
        }

    }
}
