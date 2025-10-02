using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class AggregationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AggregationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAggregates")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAggregates()
        {
            //string[] employees = await _mediator.Send(new GetAllEmployees());
            return Ok();
        }
    }
}
