using OmniCarPark.Domain;

namespace OmniCarPark.Application.Services.Interfaces;

public interface IFeeCalculator
{
    double CalculateFee(DateTime startTime, DateTime endTime, VehicleType vehicleType);
}