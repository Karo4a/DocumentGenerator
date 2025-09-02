namespace DocumentGenerator.Services.Contracts.Exceptions
{
    /// <summary>
    /// Ошибки валидации
    /// </summary>
    public class DocumentGeneratorValidationException : DocumentGeneratorException
    {
        /// <summary>
        /// Список ошибок
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentGeneratorValidationException(IEnumerable<InvalidateItemModel> errors)
        {
            Errors = errors;
        }
    }
}
