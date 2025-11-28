namespace ToDo_backend.Application.Common.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}   