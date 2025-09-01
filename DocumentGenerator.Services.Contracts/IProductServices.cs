using DocumentGenerator.Services.Contracts.Models;

namespace DocumentGenerator.Services
{
    /// <summary>
    /// Сервис по работе с товарами
    /// </summary>
    public interface IProductServices
    {
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
        Task<ProductModel> Edit(ProductModel model, CancellationToken cancellationToken);
    }
}
