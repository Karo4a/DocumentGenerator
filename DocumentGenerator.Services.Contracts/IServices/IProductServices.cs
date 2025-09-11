using DocumentGenerator.Services.Contracts.Models.Product;

namespace DocumentGenerator.Services.Contracts.IServices
{
    /// <summary>
    /// Сервис по работе с товарами
    /// </summary>
    public interface IProductServices
    {
        /// <summary>
        /// Возвращает <see cref="ProductModel"/> по идентификатору
        /// </summary>
        Task<ProductModel> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает список <see cref="ProductModel"/>
        /// </summary>
        Task<IReadOnlyCollection<ProductModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="ProductModel"/> 
        /// </summary>
        Task<ProductModel> Create(ProductCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий <see cref="ProductModel"/>
        /// </summary>
        Task<ProductModel> Edit(Guid id, ProductCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий <see cref="ProductModel"/> из базы данных
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
