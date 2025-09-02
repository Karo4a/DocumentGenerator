namespace DocumentGenerator.Services.Contracts.Exceptions
{
    /// <summary>
    /// Исключение недопустимой операции 
    /// </summary>
    public class DocumentGeneratorInvalidOperationException : DocumentGeneratorException
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentGeneratorInvalidOperationException(string message)
            : base(message) { }
    }
}
