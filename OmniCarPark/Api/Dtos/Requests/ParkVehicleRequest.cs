using OmniCarPark.Domain;

namespace OmniCarPark.Api.Dtos.Requests;

public class ParkVehicleRequest
{
    public required string VehicleReg { get; init; }
    public required VehicleType VehicleType { get; init; }
}