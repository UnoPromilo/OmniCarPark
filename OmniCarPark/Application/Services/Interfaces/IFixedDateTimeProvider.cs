namespace OmniCarPark.Application.Services.Interfaces;

public interface IFixedDateTimeProvider
{
    DateTime? FixedDateTime { get; }
    void SetFixedDateTime(DateTime time);
    void Reset();
}