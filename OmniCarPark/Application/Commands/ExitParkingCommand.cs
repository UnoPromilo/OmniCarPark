using OmniCarPark.Domain;
using OneOf;

namespace OmniCarPark.Application.Commands;

public class ExitParkingCommand
{
    public RegistrationNumber RegistrationNumber { get; }

    private ExitParkingCommand(RegistrationNumber registrationNumber)
    {
        RegistrationNumber = registrationNumber;
    }

    public static OneOf<ExitParkingCommand, RegistrationNumber.Error> TryCreate(
        string rawRegistrationNumber)
    {
        var registrationNumberResult = RegistrationNumber.TryCreate(rawRegistrationNumber);

        return registrationNumberResult
            .Match<OneOf<ExitParkingCommand, RegistrationNumber.Error>>(
                number => new ExitParkingCommand(number),
                error => error);
    }
}