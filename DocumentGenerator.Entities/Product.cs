using DocumentGenerator.Context.Contracts;

namespace DocumentGenerator.Entities
{
    /// <summary>
    /// Сущность товара
    /// </summary>
    public class Product : IEntityWithId, IEntityWithAudit, IEntityWithSoftDeleted
    {
        /// <inheritdoc cref="IEntityWithId.Id"/>
        public Guid Id { get; set; }

        /// <summary>
        /// Название записи
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Описание записи
        /// </summary>
        public string Description { get; set; } = String.Empty;

        /// <inheritdoc cref="IEntityWithAudit.CreatedAt"/>
        public DateTimeOffset CreatedAt { get; set; }

        /// <inheritdoc cref="IEntityWithAudit.UpdatedAt"/>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <inheritdoc cref="IEntityWithSoftDeleted.DeletedAt"/>
        public DateTimeOffset? DeletedAt { get; set; }
       
    }
}
