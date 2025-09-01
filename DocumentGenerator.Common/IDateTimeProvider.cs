namespace DocumentGenerator.Common
{
    /// <summary>
    /// Интерфейс поставщика времени
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Возвращает глобальное время относительно UTC
        /// </summary>
        DateTimeOffset UtcNow();
        
        /// <summary>
        /// Возвращает локальное время
        /// </summary>
        DateTimeOffset Now();
    }
}
