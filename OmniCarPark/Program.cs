using Microsoft.EntityFrameworkCore;
using OmniCarPark.Application.CommandHandlers;
using OmniCarPark.Application.CommandHandlers.Interfaces;
using OmniCarPark.Application.Commands;
using OmniCarPark.Application.Queries;
using OmniCarPark.Application.QueryHandlers;
using OmniCarPark.Application.QueryHandlers.Interfaces;
using OmniCarPark.Application.Repositories.Concrete;
using OmniCarPark.Application.Repositories.Interfaces;
using OmniCarPark.Application.Results;
using OmniCarPark.Application.Services.Concrete;
using OmniCarPark.Application.Services.Interfaces;
using OmniCarPark.Domain;
using OmniCarPark.Infrastructure.Data;
using OneOf.Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddTransient<ICommandHandler<RestoreDatabaseCommand, Success>, RestoreDatabaseCommandHandler>();
builder.Services.AddTransient<ICommandHandler<ParkVehicleCommand, ParkVehicleResult>, ParkVehicleCommandHandler>();
builder.Services.AddTransient<ICommandHandler<ExitParkingCommand, ExitParkingResult>, ExitParkingCommandHandler>();

builder.Services.AddTransient<IQueryHandler<ParkingInfoQuery, ParkingInfo>, ParkingInfoQueryHandler>();

builder.Services.AddTransient<IFeeCalculator, FeeCalculator>();
builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IFixedDateTimeProvider, FixedDateTimeProvider>();

builder.Services.AddTransient<IParkingSpaceRepository, ParkingSpaceRepository>();
builder.Services.AddTransient<IParkingEntryRepository, ParkingEntryRepository>();

var app = builder.Build();

{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();