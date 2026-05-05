namespace DocumentGenerator.Services.Contracts.Exceptions
{
    /// <summary>
    /// Исключение найденного повторения
    /// </summary>
    public class DocumentGeneratorDuplicateException : DocumentGeneratorException
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentGeneratorDuplicateException(string message)
            : base(message) { }
    }
}
