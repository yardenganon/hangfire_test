using Microsoft.AspNetCore.Mvc;

namespace AttributionService.Controllers;

[ApiController]
[Route("[controller]")]
public class AttributionController : ControllerBase
{
    [HttpGet(Name = "AddRecurringJob")]
    public IActionResult AddRecurringJob() => Ok();
}
