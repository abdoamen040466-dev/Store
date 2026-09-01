using Microsoft.AspNetCore.Mvc;

namespace Store.Presentation.API;

[ApiController]
[Route("api/[controller]")]
public class BuggyController : ControllerBase
{
    [HttpGet("notfound")]
    public IActionResult GetNotFoundResponse()
    {
        // logic
        return NotFound();
    }

    [HttpGet("BadRequest")]
    public IActionResult GetBadRequest()
    {
        // logic
        return BadRequest();
    }

    [HttpGet("BadRequest/{id}")]
    public IActionResult GetValidationErrorRequest(int id)
    {
        // logic
        return BadRequest();
    }

    [HttpGet("servererror")]
    public IActionResult GetServerError()
    {
        // logic
        throw new Exception();
        return BadRequest();
    }

    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorizedResponse()
    {
        // logic
        return Unauthorized();
    }


}
