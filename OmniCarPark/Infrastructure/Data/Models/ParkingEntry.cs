using System.ComponentModel.DataAnnotations;
using OmniCarPark.Domain;

namespace OmniCarPark.Infrastructure.Data.Models;

public class ParkingEntry
{
    public Guid Id { get; set; }
    [MaxLength(20)] public string RegistrationPlate { get; set; } = null!;
    public int ParkingSpaceId { get; set; }
    public VehicleType VehicleType { get; set; }
    public ParkingSpace ParkingSpace { get; set; } = null!;
    public DateTime ParkingEntryDateTime { get; set; }
    public DateTime? ParkingExitDateTime { get; set; }
}