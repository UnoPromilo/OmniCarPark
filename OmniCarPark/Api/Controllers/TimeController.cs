using Microsoft.AspNetCore.Mvc;
using OmniCarPark.Api.Dtos.Requests;
using OmniCarPark.Api.Dtos.Responses;
using OmniCarPark.Application.Services.Interfaces;

namespace OmniCarPark.Api.Controllers;

[ApiController]
public class TimeController(IFixedDateTimeProvider fixedDateTimeProvider) : ControllerBase
{
    [HttpPost]
    [Route("internal/time/set")]
    public Task<IActionResult> SetStaticTime([FromBody] SetFixedTimeRequest request)
    {
        fixedDateTimeProvider.SetFixedDateTime(request.FixedTime);
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpPost]
    [Route("internal/time/reset")]
    public Task<IActionResult> ResetStaticTime()
    {
        fixedDateTimeProvider.Reset();
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpGet]
    [Route("internal/time/get")]
    public Task<IActionResult> GetStaticTime()
    {
        return Task.FromResult<IActionResult>(Ok(
            new GetTimeResponse
            {
                UtcNow = fixedDateTimeProvider.FixedDateTime ?? DateTime.UtcNow
            }));
    }
}