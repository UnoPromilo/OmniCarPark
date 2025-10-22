using OmniCarPark.Domain;
using OneOf;

namespace OmniCarPark.Application.Results;

[GenerateOneOf]
public partial class ExitParkingResult : OneOfBase<ExitParkingResult.Success, ExitParkingResult.VehicleIsNotParked>
{
    public class Success
    {
        public required RegistrationNumber RegistrationNumber { get; init; }
        public required DateTime TimeIn { get; init; }
        public required DateTime TimeOut { get; init; }
        public required double Fee { get; init; }
    }

    public struct VehicleIsNotParked;
}