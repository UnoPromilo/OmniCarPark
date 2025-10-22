using Microsoft.EntityFrameworkCore;
using OmniCarPark.Application.Repositories.Interfaces;
using OmniCarPark.Domain;
using OmniCarPark.Infrastructure.Data;
using OmniCarPark.Infrastructure.Data.Models;

namespace OmniCarPark.Application.Repositories.Concrete;

public class ParkingEntryRepository(AppDbContext context) : IParkingEntryRepository
{
    public async Task<ParkingEntry?> GetNotFinishedParkingEntryOrNull(RegistrationNumber registrationNumber)
    {
        return await context.ParkEntries.Where(e =>
            e.ParkingExitDateTime == null && e.RegistrationPlate == registrationNumber.Value).FirstOrDefaultAsync();
    }
}