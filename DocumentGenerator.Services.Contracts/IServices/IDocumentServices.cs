using DocumentGenerator.Services.Contracts.Models.Document;

namespace DocumentGenerator.Services.Contracts.IServices
{
    /// <summary>
    /// Сервис по работе с документами
    /// </summary>
    public interface IDocumentServices
    {
        /// <summary>
        /// Возвращает список <see cref="DocumentModel"/>
        /// </summary>
        Task<IReadOnlyCollection<DocumentModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="DocumentModel"/> 
        /// </summary>
        Task<DocumentModel> Create(DocumentCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий <see cref="DocumentModel"/>
        /// </summary>
        Task<DocumentModel> Edit(Guid id, DocumentCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий <see cref="DocumentModel"/> из базы данных
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
