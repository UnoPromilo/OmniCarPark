using OmniCarPark.Application.Services.Interfaces;

namespace OmniCarPark.Application.Services.Concrete;

public class FixedDateTimeProvider : IFixedDateTimeProvider
{
    public DateTime? FixedDateTime { get; private set; }

    public void SetFixedDateTime(DateTime time)
    {
        FixedDateTime = time;
    }

    public void Reset()
    {
        FixedDateTime = null;
    }
}