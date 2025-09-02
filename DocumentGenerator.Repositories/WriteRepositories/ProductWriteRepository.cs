using DocumentGenerator.Common;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;

namespace DocumentGenerator.Repositories.WriteRepositories
{
    /// <inheritdoc cref="IProductWriteRepository" />
    public class ProductWriteRepository : BaseWriteRepository<Product>, IProductWriteRepository
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
            : base(writer, dateTimeProvider)
        {

        }
    }
}
