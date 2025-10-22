using OmniCarPark.Domain;
using OneOf;

namespace OmniCarPark.Application.Commands;

public class ParkVehicleCommand
{
    public RegistrationNumber RegistrationNumber { get; }
    public VehicleType VehicleType { get; }

    private ParkVehicleCommand(RegistrationNumber registrationNumber, VehicleType vehicleType)
    {
        RegistrationNumber = registrationNumber;
        VehicleType = vehicleType;
    }

    public static OneOf<ParkVehicleCommand, RegistrationNumber.Error, InvalidVehicleType> TryCreate(
        string rawRegistrationNumber, VehicleType vehicleType)
    {
        if (!Enum.IsDefined(vehicleType))
        {
            return new InvalidVehicleType();
        }

        var registrationNumberResult = RegistrationNumber.TryCreate(rawRegistrationNumber);

        return registrationNumberResult
            .Match<OneOf<ParkVehicleCommand, RegistrationNumber.Error, InvalidVehicleType>>(
                number => new ParkVehicleCommand(number, vehicleType),
                error => error);
    }
}

public struct InvalidVehicleType;