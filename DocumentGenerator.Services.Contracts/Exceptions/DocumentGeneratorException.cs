namespace DocumentGenerator.Services.Contracts.Exceptions
{
    /// <summary>
    /// Базовый класс исключений
    /// </summary>
    public abstract class DocumentGeneratorException : Exception
    {
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        protected DocumentGeneratorException() { }

        /// <summary>
        /// Конструктор c указанием сообщения об ошибке
        /// </summary>
        protected DocumentGeneratorException(string message)
            : base(message) { }
    }
}
