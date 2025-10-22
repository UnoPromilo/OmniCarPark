using OmniCarPark.Application.Services.Interfaces;

namespace OmniCarPark.Application.Services.Concrete;

public class DateTimeProvider(IFixedDateTimeProvider fixedDateTimeProvider) : IDateTimeProvider
{
    // Close enough
    private readonly DateTime _requestDateTime = fixedDateTimeProvider.FixedDateTime ?? DateTime.UtcNow;

    public DateTime RequestTimeUtc()
    {
        return _requestDateTime;
    }
}