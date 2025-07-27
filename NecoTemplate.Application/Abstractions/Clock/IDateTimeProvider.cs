namespace NecoTemplate.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime UTCNow { get; }
    string PersianNow { get; }
}
