using ToDo_backend.Application.Common.Abstractions.Clock;

namespace RideHubb.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    #region Constants
    private const int UTCColombiaOffset = -5;
    #endregion

    public DateTime UtcNow => DateTime.UtcNow.ToUniversalTime().AddHours(UTCColombiaOffset);
}