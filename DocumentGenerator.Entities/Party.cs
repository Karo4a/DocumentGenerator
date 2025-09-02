
using DocumentGenerator.Context.Contracts;

namespace DocumentGenerator.Entities
{
    /// <summary>
    /// Сущность стороны акта
    /// </summary>
    public class Party : IEntityWithId, IEntityWithAudit, IEntityWithSoftDeleted
    {
        /// <inheritdoc cref="IEntityWithId.Id"/>
        public Guid Id { get; set; }

        /// <summary>
        /// Полное имя
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Должность
        /// </summary>
        public string Job { get; set; } = string.Empty;

        /// <summary>
        /// ИНН
        /// </summary>
        public string TaxId { get; set; } = string.Empty;

        /// <inheritdoc cref="IEntityWithAudit.CreatedAt"/>
        public DateTimeOffset CreatedAt { get; set; }

        /// <inheritdoc cref="IEntityWithAudit.UpdatedAt"/>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <inheritdoc cref="IEntityWithSoftDeleted.DeletedAt"/>
        public DateTimeOffset? DeletedAt { get; set; }

    }
}
