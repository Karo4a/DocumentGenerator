using DocumentGenerator.Entities.Contracts;

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
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <inheritdoc cref="IEntityWithAudit.CreatedAt"/>
        public DateTimeOffset CreatedAt { get; set; }

        /// <inheritdoc cref="IEntityWithAudit.UpdatedAt"/>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <inheritdoc cref="IEntityWithSoftDeleted.DeletedAt"/>
        public DateTimeOffset? DeletedAt { get; set; }
       
    }
}
