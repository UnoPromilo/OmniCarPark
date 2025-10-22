using System.ComponentModel.DataAnnotations;

namespace OmniCarPark.Infrastructure.Data.Models;

public class ParkingEntry
{
    public Guid Id { get; set; }
    [MaxLength(20)] public string RegistrationPlate { get; set; } = null!;
    public Guid ParkingSpaceId { get; set; }
    public ParkingSpace ParkingSpace { get; set; } = null!;
    public DateTime ParkingEntryDate { get; set; }
    public DateTime? ParkingExitDate { get; set; }
}