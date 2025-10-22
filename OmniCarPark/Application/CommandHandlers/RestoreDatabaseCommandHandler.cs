using Microsoft.EntityFrameworkCore;
using OmniCarPark.Application.CommandHandlers.Interfaces;
using OmniCarPark.Application.Commands;
using OmniCarPark.Infrastructure.Data;
using OmniCarPark.Infrastructure.Data.Models;
using OneOf.Types;

namespace OmniCarPark.Application.CommandHandlers;

public class RestoreDatabaseCommandHandler(
    AppDbContext dbContext) : ICommandHandler<RestoreDatabaseCommand, Success>
{
    // Not the cleanest method in the world but it is enough
    public async Task<Success> Handle(RestoreDatabaseCommand command)
    {
        await dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM \"ParkingSpaces\"");

        var newParkingSpaces = Enumerable.Range(1, command.AvailableParkingSpaces)
            .Select(i => new ParkingSpace
            {
                Id = i,
            }).ToList();

        dbContext.ParkingSpaces.AddRange(newParkingSpaces);
        await dbContext.SaveChangesAsync();
        return new();
    }
}