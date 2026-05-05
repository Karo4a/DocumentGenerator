namespace DocumentGenerator.Services.Contracts.Exceptions
{
    /// <summary>
    /// Модель инвалидации модели
    /// </summary>
    public class InvalidateItemModel
    {

        /// <summary>
        /// Создаёт <see cref="InvalidateItemModel"/>
        /// </summary>
        public static InvalidateItemModel New(string field, string message)
            => new InvalidateItemModel(field, message);

        /// <summary>
        /// Конструктор
        /// </summary>
        private InvalidateItemModel(string field, string message)
        {
            Field = field;
            Message = message;
        }

        /// <summary>
        /// Имя инвалидного поля
        /// </summary>
        /// <remarks>Если пустое, значит инвалидация относится ко всей модели</remarks>
        public string Field { get; } = string.Empty;

        /// <summary>
        /// Сообщение инвалидации
        /// </summary>
        public string Message { get; } = string.Empty;
    }
}
