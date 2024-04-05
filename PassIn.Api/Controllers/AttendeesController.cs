namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        [HttpPost]
        [Route("register/{id}")]
        [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public IActionResult Register([FromRoute] Guid eventId, [FromBody] RequestRegisterEventJson request)
        {
            var useCase = new RegisterAttendeeOnEventUseCase();

            var response = useCase.Execute(eventId, request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [Route("{eventId}")]
        [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetAll([FromRoute] Guid eventId)
        {
            var useCase = new GetAllAttendeesByEventIdUseCase();

            var response = useCase.Execute(eventId);

            return Ok(response);
        }

    }
}
