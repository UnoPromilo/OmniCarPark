using OmniCarPark.Application.Queries;
using OmniCarPark.Application.QueryHandlers.Interfaces;
using OmniCarPark.Application.Repositories.Interfaces;
using OmniCarPark.Domain;

namespace OmniCarPark.Application.QueryHandlers;

public class ParkingInfoQueryHandler(IParkingSpaceRepository repository) : IQueryHandler<ParkingInfoQuery, ParkingInfo>
{
    public async Task<ParkingInfo> Handle(ParkingInfoQuery command)
    {
        return new()
        {
            AvailableSpaces = await repository.GetFreeSpaces(),
            TakenSpaces = await repository.GetTakenSpaces()
        };
    }
}