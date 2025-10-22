namespace OmniCarPark.Tests.DTOs;

public class ParkingPostRequest
{
    public required string VehicleReg { get; init; }
    public required int VehicleType { get; init; }
}