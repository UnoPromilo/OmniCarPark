namespace OmniCarPark.Tests.DTOs;

public class ParkingGetResponse
{
    public required int AvailableSpaces { get; init; }
    public required int OccupiedSpaces { get; init; }
}