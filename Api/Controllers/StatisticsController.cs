using Application.Queries.Statistics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetStatistics")]
        [ProducesResponseType(typeof(IReadOnlyDictionary<string, StatisticsModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStatistics()
        {
            IReadOnlyDictionary<string, StatisticsModel> result = await _mediator.Send(new GetStatistics());
            return Ok(result);
        }
    }
}
