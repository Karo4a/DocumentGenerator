using DocumentGenerator.Services.Contracts.Models.Party;

namespace DocumentGenerator.Services
{
    /// <summary>
    /// Сервис по работе с товарами
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
        Task<PartyModel> Edit(PartyModel model, CancellationToken cancellationToken);
    }
}
