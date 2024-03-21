using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers;

[Route("health")]
[ApiController]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        Console.WriteLine("GG");
        
        return Ok("We are ok");
    }
}