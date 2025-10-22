using System.ComponentModel.DataAnnotations;

namespace OmniCarPark.Api.Dtos.Requests;

public class RestoreDatabaseRequest
{
    [Required]
    public required int AvailableParkingSpaces { get; init; }
}