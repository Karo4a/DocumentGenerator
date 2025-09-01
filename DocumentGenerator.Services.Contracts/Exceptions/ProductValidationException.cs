namespace DocumentGenerator.Services.Contracts.Exceptions
{
    /// <summary>
    /// Ошибки валидации
    /// </summary>
    public class ProductValidationException : ProductException
    {
        /// <summary>
        /// Список ошибок
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductValidationException(IEnumerable<InvalidateItemModel> errors)
        {
            Errors = errors;
        }
    }
}
