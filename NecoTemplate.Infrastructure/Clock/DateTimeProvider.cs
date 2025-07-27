using NecoTemplate.Application.Abstractions.Clock;

namespace NecoTemplate.Infrastructure.Clock;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UTCNow => DateTime.UtcNow;

    public string PersianNow => throw new NotImplementedException();
}
