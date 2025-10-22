namespace OmniCarPark.Infrastructure.Data.Models;

public class ParkingSpace
{
    public Guid Id { get; set; }
    public ICollection<ParkingEntry> ParkingEntries { get; set; }
}