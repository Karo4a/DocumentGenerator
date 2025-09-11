using DocumentGenerator.Services.Contracts.Models.Document;

namespace DocumentGenerator.Services.Contracts.IServices
{
    /// <summary>
    /// Сервис экспорта для Excel
    /// </summary>
    public interface IExcelServices
    {
        /// <summary>
        /// Экспортирует <see cref="DocumentModel"/> в Excel
        /// </summary>
        MemoryStream Export(DocumentModel documentModel, CancellationToken cancellationToken);
    }
}
