using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;

namespace DocumentGenerator.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи <see cref="Product"/>
    /// </summary>
    public interface IProductWriteRepository : IDbWriter<Product>
    {
    }
}
