using OmniCarPark.Domain;
using OmniCarPark.Infrastructure.Data.Models;

namespace OmniCarPark.Application.Repositories.Interfaces;

public interface IParkingSpaceRepository
{
    Task<ParkingSpace?> GetFirstParkingSpaceOrNull();

    void AllocateParkingSpace(
        ParkingSpace parkingSpace,
        RegistrationNumber registrationNumber,
        VehicleType vehicleType,
        DateTime parkTimeUtc);

    Task<int> GetFreeSpaces();
    Task<int> GetTakenSpaces();
}