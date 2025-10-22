using OmniCarPark.Application.Services.Concrete;
using OmniCarPark.Domain;

namespace OmniCarPark.UnitTests.App.Services;

public class FeeCalculatorTests
{
    private FeeCalculator _calc;

    [SetUp]
    public void SetUp()
    {
        _calc = new();
    }

    [TestCase(5, VehicleType.Small, 5 * 0.1 + 1)]
    [TestCase(5, VehicleType.Medium, 5 * 0.2 + 1)]
    [TestCase(5, VehicleType.Large, 5 * 0.4 + 1)]
    [TestCase(10, VehicleType.Small, 10 * 0.1 + 2)]
    [TestCase(10, VehicleType.Medium, 10 * 0.2 + 2)]
    [TestCase(10, VehicleType.Large, 10 * 0.4 + 2)]
    [TestCase(7, VehicleType.Small, 7 * 0.1 + 1)]
    [TestCase(12, VehicleType.Medium, 12 * 0.2 + 2)]
    [TestCase(17, VehicleType.Large, 17 * 0.4 + 3)]
    public void CalculateFee_ReturnsExpected(double minutes, VehicleType type, double expected)
    {
        var start = new DateTime(2025, 1, 1, 10, 0, 0);
        var end = start.AddMinutes(minutes);
        var result = _calc.CalculateFee(start, end, type);
        Assert.That(result, Is.EqualTo(expected).Within(0.001));
    }
}