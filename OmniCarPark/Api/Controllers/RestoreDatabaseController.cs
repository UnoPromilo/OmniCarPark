using Microsoft.AspNetCore.Mvc;
using OmniCarPark.Api.Dtos.Requests;
using OmniCarPark.Application.CommandHandlers.Interfaces;
using OmniCarPark.Application.Commands;
using OneOf.Types;

namespace OmniCarPark.Api.Controllers;

[ApiController]
[Route("internal/restore")]
public class RestoreDatabaseController(ICommandHandler<RestoreDatabaseCommand, Success> commandHandler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RestoreDatabase([FromBody] RestoreDatabaseRequest request)
    {
        var command = new RestoreDatabaseCommand
        {
            AvailableParkingSpaces = request.AvailableParkingSpaces
        };
        await commandHandler.Handle(command);
        return Ok();
    }
}