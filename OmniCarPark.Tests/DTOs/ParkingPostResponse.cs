namespace OmniCarPark.Tests.DTOs;

public class ParkingPostResponse
{
    public required string VehicleReg { get; init; }
    public required int SpaceNumber { get; init; }
    public required DateTime TimeIn { get; init; }
}