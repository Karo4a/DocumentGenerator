using DocumentGenerator.Services.Contracts.Models.Party;

namespace DocumentGenerator.Services.Contracts.IServices
{
    /// <summary>
    /// Сервис по работе со сторонами актов
    /// </summary>
    public interface IPartyServices
    {
        /// <summary>
        /// Возвращает список <see cref="PartyModel"/>
        /// </summary>
        Task<IReadOnlyCollection<PartyModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="PartyModel"/> 
        /// </summary>
        Task<PartyModel> Create(PartyCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий <see cref="PartyModel"/>
        /// </summary>
        Task<PartyModel> Edit(Guid id, PartyCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий <see cref="PartyModel"/> из базы данных
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
