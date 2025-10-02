using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/aggregation")]
    public class AggregationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AggregationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAggregates")]
        [ProducesResponseType(typeof(AggregationModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAggregates()
        {
            AggregationModel result = await _mediator.Send(new GetAggregates());
            return Ok(result);
        }
    }
}
