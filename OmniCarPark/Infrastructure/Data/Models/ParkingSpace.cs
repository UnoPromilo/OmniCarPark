namespace OmniCarPark.Infrastructure.Data.Models;

public class ParkingSpace
{
    public int Id { get; set; }
    public ICollection<ParkingEntry> ParkingEntries { get; set; }
}