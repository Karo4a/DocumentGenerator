using DocumentGenerator.Services.Contracts.Exceptions;

namespace DocumentGenerator.Web.Models.Exceptions
{
    /// <summary>
    /// Информация об ошибке валидации работы API
    /// </summary>
    public class ApiValidationExceptionDetail
    {
        /// <summary>
        /// Ошибки валидации
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; set; } = Array.Empty<InvalidateItemModel>();
    }
}
