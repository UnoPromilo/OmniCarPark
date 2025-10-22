namespace OmniCarPark.Domain;

public class ParkingInfo
{
    public required int AvailableSpaces { get; init; }

    public required int TakenSpaces { get; init; }
}