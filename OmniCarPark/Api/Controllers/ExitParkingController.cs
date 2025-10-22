using Microsoft.AspNetCore.Mvc;
using OmniCarPark.Api.Dtos.Requests;
using OmniCarPark.Api.Dtos.Responses;
using OmniCarPark.Application.CommandHandlers.Interfaces;
using OmniCarPark.Application.Commands;
using OmniCarPark.Application.Results;
using OmniCarPark.Domain;

namespace OmniCarPark.Api.Controllers;

[ApiController]
public class ExitParkingController(ICommandHandler<ExitParkingCommand, ExitParkingResult> commandHandler)
    : ControllerBase
{
    [HttpPost]
    [Route("parking/exit")]
    public Task<IActionResult> Exit([FromBody] ExitParkingRequest request)
    {
        var commandParseResult = ExitParkingCommand.TryCreate(request.VehicleReg);
        return commandParseResult.Match(HandleCommand, BadRequest);
    }

    private async Task<IActionResult> HandleCommand(ExitParkingCommand command)
    {
        var commandResult = await commandHandler.Handle(command);
        return commandResult.Match(Ok, Unprocessable);
    }

    private IActionResult Ok(ExitParkingResult.Success success)
    {
        return Ok(new ExitParkingResponse
        {
            VehicleReg = success.RegistrationNumber.Value,
            VehicleCharge = success.Fee,
            TimeIn = success.TimeIn,
            TimeOut = success.TimeOut,
        });
    }

    private static Task<IActionResult> BadRequest(RegistrationNumber.Error error)
    {
        return Task.FromResult<IActionResult>(new BadRequestObjectResult($"Invalid vehicle registration {error}"));
    }

    private static IActionResult Unprocessable(ExitParkingResult.VehicleIsNotParked error)
    {
        return new UnprocessableEntityObjectResult(error);
    }
}