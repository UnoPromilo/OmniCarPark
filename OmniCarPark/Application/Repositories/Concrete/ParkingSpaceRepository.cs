using Microsoft.EntityFrameworkCore;
using OmniCarPark.Application.Repositories.Interfaces;
using OmniCarPark.Domain;
using OmniCarPark.Infrastructure.Data;
using OmniCarPark.Infrastructure.Data.Models;

namespace OmniCarPark.Application.Repositories.Concrete;

public class ParkingSpaceRepository(AppDbContext context) : IParkingSpaceRepository
{
    public async Task<ParkingSpace?> GetFirstParkingSpaceOrNull()
    {
        return await context.ParkingSpaces.Where(p => p.ParkingEntries.All(e => e.ParkingExitDateTime != null))
            .OrderBy(p => p.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> GetFreeSpaces()
    {
        return await context.ParkingSpaces.Where(p => p.ParkingEntries.All(e => e.ParkingExitDateTime != null))
            .CountAsync();
    }

    public async Task<int> GetTakenSpaces()
    {
        return await context.ParkingSpaces.Where(p => p.ParkingEntries.Any(e => e.ParkingExitDateTime == null))
            .CountAsync();
    }

    public void AllocateParkingSpace(
        ParkingSpace parkingSpace,
        RegistrationNumber registrationNumber,
        VehicleType vehicleType,
        DateTime parkTimeUtc)
    {
        context.ParkEntries.Add(new()
        {
            ParkingEntryDateTime = parkTimeUtc,
            ParkingSpace = parkingSpace,
            RegistrationPlate = registrationNumber.Value,
            VehicleType = vehicleType
        });
    }
}