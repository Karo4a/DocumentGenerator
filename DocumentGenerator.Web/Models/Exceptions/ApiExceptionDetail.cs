namespace DocumentGenerator.Web.Models.Exceptions
{
    /// <summary>
    /// Информация об ошибке работы API
    /// </summary>
    public class ApiExceptionDetail
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ApiExceptionDetail(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Сообщение ошибки
        /// </summary>
        string Message { get; }
    }
}
