using DocumentGenerator.Common.Contracts;

namespace DocumentGenerator.Common
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
