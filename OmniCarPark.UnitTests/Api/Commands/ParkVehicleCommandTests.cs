using OmniCarPark.Application.Commands;
using OmniCarPark.Domain;

namespace OmniCarPark.UnitTests.Api.Commands;

public class ParkVehicleCommandTests
{
    [TestCase(9999)]
    [TestCase(0)]
    [TestCase(4)]
    [TestCase(-1)]
    public void ValidateAsync_ShouldReturnInvalid_WhenVehicleTypeIsNotDefined(int vehicleType)
    {
        var result = ParkVehicleCommand.TryCreate("AB123", (VehicleType)vehicleType);

        Assert.That(result.Value, Is.InstanceOf<InvalidVehicleType>());
    }

    [TestCase("A1", RegistrationNumber.Error.TooShort)]
    [TestCase("ABCDEFGHIJKLMNOPQRSTU", RegistrationNumber.Error.TooLong)]
    [TestCase("AB@123", RegistrationNumber.Error.InvalidValue)]
    [TestCase("AB 123", RegistrationNumber.Error.InvalidValue)]
    [TestCase("AB-123", RegistrationNumber.Error.InvalidValue)]
    [TestCase("💩💩💩💩💩", RegistrationNumber.Error.InvalidValue)]
    public void ValidateAsync_ShouldReturnInvalid_ForBadVehicleReg(string vehicleReg, RegistrationNumber.Error error)
    {
        var result = ParkVehicleCommand.TryCreate(vehicleReg, VehicleType.Small);

        Assert.Multiple(() =>
        {
            Assert.That(result.Value, Is.InstanceOf<RegistrationNumber.Error>());
            Assert.That(result.Value, Is.EqualTo(error));
        });
    }

    [TestCase("AB123", VehicleType.Large)]
    public void ValidateAsync_ShouldReturnValid_WhenAllInputsAreCorrect(string vehicleReg,
        VehicleType vehicleType)
    {
        var result = ParkVehicleCommand.TryCreate(vehicleReg, vehicleType);


        Assert.That(result.Value, Is.InstanceOf<ParkVehicleCommand>());
    }
}