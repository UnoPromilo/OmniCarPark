using Microsoft.AspNetCore.Mvc;

namespace OmniCarPark.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [Route("/health")]
    public Task<IActionResult> Get()
    {
        return Task.FromResult<IActionResult>(Ok("Ok"));
    }
}