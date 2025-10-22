using OmniCarPark.Application.Services.Interfaces;
using OmniCarPark.Domain;

namespace OmniCarPark.Application.Services.Concrete;

public class FeeCalculator : IFeeCalculator
{
    public double CalculateFee(DateTime startTime, DateTime endTime, VehicleType vehicleType)
    {
        var totalMinutes = double.Ceiling((endTime - startTime).TotalMinutes);
        var fiveMinutes = (int)double.Floor(totalMinutes / 5);
        // It is decreasing the number of floating point math errors but not reducing to zero.
        // The best approach will be to return then as int and divide by 100 only on the client side.
        var totalFee100 = (int)totalMinutes * PerMinuteFee(vehicleType) + fiveMinutes * 100;
        return totalFee100 / 100.0;
    }

    private static int PerMinuteFee(VehicleType vehicleType)
    {
        return vehicleType switch
        {
            VehicleType.Small => 10,
            VehicleType.Medium => 20,
            VehicleType.Large => 40,
            _ => throw new ArgumentOutOfRangeException(nameof(vehicleType), vehicleType, null)
        };
    }
}