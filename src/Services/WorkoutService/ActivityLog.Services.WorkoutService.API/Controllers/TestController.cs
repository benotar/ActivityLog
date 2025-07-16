using Microsoft.AspNetCore.Mvc;

namespace ActivityLog.Services.WorkoutService.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok($"Workout service is alive! :)\nUtc Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
    }
}
