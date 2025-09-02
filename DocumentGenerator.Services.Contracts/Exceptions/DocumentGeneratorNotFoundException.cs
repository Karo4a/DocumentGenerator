namespace DocumentGenerator.Services.Contracts.Exceptions
{
    /// <summary>
    /// Исключение не найденного продукта
    /// </summary>
    public class DocumentGeneratorNotFoundException : DocumentGeneratorException
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentGeneratorNotFoundException(string message)
            : base(message) { }
    }
}
