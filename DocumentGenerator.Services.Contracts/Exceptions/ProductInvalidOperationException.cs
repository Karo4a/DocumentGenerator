namespace DocumentGenerator.Services.Contracts.Exceptions
{
    /// <summary>
    /// Исключение недопустимой операции 
    /// </summary>
    public class ProductInvalidOperationException : ProductException
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductInvalidOperationException(string message)
            : base(message) { }
    }
}
