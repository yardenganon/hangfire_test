using Microsoft.AspNetCore.Mvc;

namespace HangFireApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SchedulingController : ControllerBase
{
    private readonly ILogger<SchedulingController> _logger;

    public SchedulingController(ILogger<SchedulingController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "AddReccuringJob")]
    public IActionResult AddReccuringJob()
    {
        return Ok();
    }
}
