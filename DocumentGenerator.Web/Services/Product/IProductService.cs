using DocumentGenerator.Api.Models.Product;
using DocumentGenerator.Web.Models;

namespace DocumentGenerator.Web.Services.Product
{
    public interface IProductService
    {
        Task<Result<ProductApiModel[]>> GetAllAsync();
        Task<Result<ProductApiModel?>> GetByIdAsync(Guid id); // Может возвращать null, если не найден
        Task<Result<ProductApiModel>> CreateAsync(ProductRequestApiModel model);
        Task<Result<ProductApiModel>> UpdateAsync(Guid id, ProductRequestApiModel model);
        Task<Result> DeleteAsync(Guid id); // Для операций без возврата данных
    }
}
