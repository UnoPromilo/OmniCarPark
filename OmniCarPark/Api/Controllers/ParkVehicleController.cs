using Microsoft.AspNetCore.Mvc;
using OmniCarPark.Api.Dtos.Requests;
using OmniCarPark.Api.Dtos.Responses;
using OmniCarPark.Application.CommandHandlers.Interfaces;
using OmniCarPark.Application.Commands;
using OmniCarPark.Application.Results;
using OmniCarPark.Application.Services.Interfaces;
using OmniCarPark.Domain;

namespace OmniCarPark.Api.Controllers;

[ApiController]
public class ParkVehicleController(
    ICommandHandler<ParkVehicleCommand, ParkVehicleResult> commandHandler,
    IDateTimeProvider dateTimeProvider) : ControllerBase
{
    [HttpPost]
    [Route("parking")]
    public Task<IActionResult> ParkVehicle(
        [FromBody] ParkVehicleRequest parkVehicleRequest)
    {
        var commandParseResult =
            ParkVehicleCommand.TryCreate(parkVehicleRequest.VehicleReg, parkVehicleRequest.VehicleType);

        return commandParseResult.Match(HandleCommand, BadRequest, BadRequest);
    }

    private async Task<IActionResult> HandleCommand(ParkVehicleCommand command)
    {
        var commandResult = await commandHandler.Handle(command);
        return commandResult.Match(space => Ok(space, command.RegistrationNumber), Conflict, Unprocessable);
    }

    private OkObjectResult Ok(SpaceNumber spaceNumber, RegistrationNumber regNumber)
    {
        return new(new ParkVehicleResponse
        {
            VehicleReg = regNumber.Value,
            SpaceNumber = spaceNumber.Value,
            TimeIn = dateTimeProvider.RequestTimeUtc()
        });
    }

    private static IActionResult Conflict(ParkVehicleResult.NoFreeSpace notFreeSpace)
    {
        return new ConflictObjectResult(notFreeSpace);
    }

    private static IActionResult Unprocessable(ParkVehicleResult.VehicleAlreadyParked vehicleAlreadyParked)
    {
        return new UnprocessableEntityObjectResult(vehicleAlreadyParked);
    }

    private static Task<IActionResult> BadRequest(RegistrationNumber.Error error)
    {
        return Task.FromResult<IActionResult>(new BadRequestObjectResult($"Invalid vehicle registration {error}"));
    }

    private static Task<IActionResult> BadRequest(InvalidVehicleType error)
    {
        return Task.FromResult<IActionResult>(new BadRequestObjectResult($"Invalid vehicle registration {error}"));
    }
}