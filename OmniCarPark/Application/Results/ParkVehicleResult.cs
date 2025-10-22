using OmniCarPark.Domain;
using OneOf;

namespace OmniCarPark.Application.Results;

[GenerateOneOf]
public partial class
    ParkVehicleResult : OneOfBase<SpaceNumber, ParkVehicleResult.NoFreeSpace, ParkVehicleResult.VehicleAlreadyParked>
{
    public struct NoFreeSpace;

    public struct VehicleAlreadyParked;
}