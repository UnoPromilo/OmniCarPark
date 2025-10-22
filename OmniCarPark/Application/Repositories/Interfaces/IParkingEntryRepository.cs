using OmniCarPark.Domain;
using OmniCarPark.Infrastructure.Data.Models;

namespace OmniCarPark.Application.Repositories.Interfaces;

public interface IParkingEntryRepository
{
    Task<ParkingEntry?> GetNotFinishedParkingEntryOrNull(RegistrationNumber registrationNumber);
}