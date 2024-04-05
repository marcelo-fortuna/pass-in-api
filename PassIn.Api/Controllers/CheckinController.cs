using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckinController : ControllerBase
    {
        [HttpPost]
        [Route("{attendeeId}")]
        [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public IActionResult Checkin([FromRoute] Guid attendeeId)
        {
            var useCase = new DoAttendeeCheckinUseCase();

            var response = useCase.Execute();

            return Created(string.Empty, response);
        }

    }
}
