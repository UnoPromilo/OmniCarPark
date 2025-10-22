using Microsoft.AspNetCore.Mvc;
using OmniCarPark.Api.Dtos.Responses;
using OmniCarPark.Application.Queries;
using OmniCarPark.Application.QueryHandlers.Interfaces;
using OmniCarPark.Domain;

namespace OmniCarPark.Api.Controllers;

[ApiController]
public class GetParkingInfoController(IQueryHandler<ParkingInfoQuery, ParkingInfo> queryHandler) : ControllerBase
{
    [HttpGet]
    [Route("parking")]
    public async Task<IActionResult> GetParkingInfo()
    {
        var parkingInfo = await queryHandler.Handle(new());
        return Ok(new GetParkInfoResponse
        {
            AvailableSpaces = parkingInfo.AvailableSpaces,
            OccupiedSpaces = parkingInfo.TakenSpaces
        });
    }
}