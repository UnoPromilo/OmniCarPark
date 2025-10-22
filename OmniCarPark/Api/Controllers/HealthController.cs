using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace OmniCarPark.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController
{
    [HttpGet]
    [Route("/health")]
    public Task<IActionResult> Get()
    {
        return Task.FromResult<IActionResult>(new OkObjectResult("Ok"));
    }
}