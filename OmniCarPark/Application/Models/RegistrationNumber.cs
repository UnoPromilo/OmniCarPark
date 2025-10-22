using OneOf;

namespace OmniCarPark.Domain;

public class RegistrationNumber
{
    private RegistrationNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static OneOf<RegistrationNumber, Error> TryCreate(string value)
    {
        switch (value.Length)
        {
            case > 20:
                return Error.TooLong;
            case < 3:
                return Error.TooShort;
        }

        if (!value.All(char.IsLetterOrDigit))
        {
            return Error.InvalidValue;
        }

        return new RegistrationNumber(value.ToUpper());
    }

    public enum Error
    {
        TooShort,
        TooLong,
        InvalidValue,
    }
}