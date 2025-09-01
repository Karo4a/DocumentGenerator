namespace DocumentGenerator.Services.Contracts.Exceptions
{
    /// <summary>
    /// Базовый класс исключений товаров
    /// </summary>
    public abstract class ProductException : Exception
    {
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        protected ProductException() { }

        /// <summary>
        /// Конструктор c указанием сообщения об ошибке
        /// </summary>
        protected ProductException(string message)
            : base(message) { }
    }
}
