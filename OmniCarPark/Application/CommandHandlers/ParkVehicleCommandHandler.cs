using OmniCarPark.Application.CommandHandlers.Interfaces;
using OmniCarPark.Application.Commands;
using OmniCarPark.Application.Repositories.Interfaces;
using OmniCarPark.Application.Results;
using OmniCarPark.Application.Services.Interfaces;
using OmniCarPark.Domain;
using OmniCarPark.Infrastructure.Data;

namespace OmniCarPark.Application.CommandHandlers;

public class ParkVehicleCommandHandler(
    IParkingSpaceRepository parkingSpaceRepository,
    IParkingEntryRepository parkingEntryRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ParkVehicleCommand, ParkVehicleResult>
{
    public async Task<ParkVehicleResult> Handle(ParkVehicleCommand command)
    {
        var parkingSpace = await parkingSpaceRepository.GetFirstParkingSpaceOrNull();
        if (parkingSpace is null)
        {
            return new ParkVehicleResult.NoFreeSpace();
        }

        if (await IsVehicleAlreadyParked(command.RegistrationNumber))
        {
            return new ParkVehicleResult.VehicleAlreadyParked();
        }

        parkingSpaceRepository.AllocateParkingSpace(
            parkingSpace,
            command.RegistrationNumber,
            command.VehicleType,
            dateTimeProvider.RequestTimeUtc());

        await unitOfWork.SaveChanges();

        return new SpaceNumber(parkingSpace.Id);
    }

    // Should be moved to separate service so it can be unit tested
    private async Task<bool> IsVehicleAlreadyParked(RegistrationNumber registrationNumber)
    {
        var entry = await parkingEntryRepository.GetNotFinishedParkingEntryOrNull(registrationNumber);
        return entry is not null;
    }
}