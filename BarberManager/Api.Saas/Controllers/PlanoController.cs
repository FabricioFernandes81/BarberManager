
using Application.Common.Pagination;
using Application.Dispatcher;
using Application.Saas.Planos.DTOs;
using Application.Saas.Planos.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Saas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly IDispatcher _dispacher;

        public PlanoController(IDispatcher dispatcher)
        {
            _dispacher = dispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> ListarPlanos([FromQuery] PaginationFilter filter)
        {
            var result = await _dispacher.Query <GetPlanosQuery,PagedResponse<List<GetPlanoDto>>>
            (new GetPlanosQuery
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
