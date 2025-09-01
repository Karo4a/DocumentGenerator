using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;

namespace DocumentGenerator.ProductRepository.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Product"/>
    /// </summary>
    public interface IProductWriteRepository : IDbWriter<Product>
    {
    }
}
