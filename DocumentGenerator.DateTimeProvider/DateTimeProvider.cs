using DocumentGenerator.Common;

namespace DocumentGenerator.DateTimeProvider
{
    /// <inheritdoc cref="IDateTimeProvider"/>
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTimeOffset IDateTimeProvider.Now()
            => DateTimeOffset.Now;

        DateTimeOffset IDateTimeProvider.UtcNow()
            => DateTimeOffset.UtcNow;
    }
}
