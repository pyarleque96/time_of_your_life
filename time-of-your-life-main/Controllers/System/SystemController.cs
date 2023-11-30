using Microsoft.AspNetCore.Mvc;

namespace time_of_your_life.Controllers;

[Route("[controller]")]
public class SystemController : BaseApiController
{
    private readonly ILogger<SystemController> _logger;

    public SystemController(ILogger<SystemController> logger)
    {
        _logger = logger;
    }

    [HttpGet("server-time")]
    public async Task<ActionResult<DateTime>> GetServerTime()
    {
        return DateTime.Now;
    }
}
