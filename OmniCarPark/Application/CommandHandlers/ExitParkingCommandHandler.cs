using OmniCarPark.Application.CommandHandlers.Interfaces;
using OmniCarPark.Application.Commands;
using OmniCarPark.Application.Repositories.Interfaces;
using OmniCarPark.Application.Results;
using OmniCarPark.Application.Services.Interfaces;
using OmniCarPark.Infrastructure.Data;

namespace OmniCarPark.Application.CommandHandlers;

public class ExitParkingCommandHandler(
    IParkingEntryRepository entryRepository,
    IDateTimeProvider dateTimeProvider,
    IFeeCalculator feeCalculator,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ExitParkingCommand, ExitParkingResult>
{
    public async Task<ExitParkingResult> Handle(ExitParkingCommand command)
    {
        var entry = await entryRepository.GetNotFinishedParkingEntryOrNull(command.RegistrationNumber);
        if (entry is null)
        {
            return new ExitParkingResult.VehicleIsNotParked();
        }

        var exitTime = dateTimeProvider.RequestTimeUtc();
        entry.ParkingExitDateTime = exitTime;

        var calculatedFee = feeCalculator.CalculateFee(entry.ParkingEntryDateTime, exitTime, entry.VehicleType);
        await unitOfWork.SaveChanges();
        return new ExitParkingResult.Success
        {
            RegistrationNumber = command.RegistrationNumber,
            TimeIn = entry.ParkingEntryDateTime,
            TimeOut = exitTime,
            Fee = calculatedFee
        };
    }
}