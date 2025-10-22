using Microsoft.EntityFrameworkCore;
using OmniCarPark.Infrastructure.Data.Models;

namespace OmniCarPark.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ParkingSpace> ParkingSpaces { get; set; }
    public DbSet<ParkingEntry> ParkEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ParkingEntry>()
            .HasOne(e => e.ParkingSpace)
            .WithMany(p => p.ParkingEntries)
            .HasForeignKey(e => e.ParkingSpaceId);

        modelBuilder.Entity<ParkingEntry>()
            .HasIndex(e => e.ParkingSpaceId)
            .HasFilter("\"ParkingExitDate\" IS NULL");

        modelBuilder.Entity<ParkingEntry>()
            .HasIndex(e => e.RegistrationPlate)
            .HasFilter("\"ParkingExitDate\" IS NULL");
    }
}