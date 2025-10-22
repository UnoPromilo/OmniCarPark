namespace OmniCarPark.Api.Dtos.Responses;

public class GetParkInfoResponse
{
    public required int AvailableSpaces { get; init; }
    public required int OccupiedSpaces { get; init; }
}