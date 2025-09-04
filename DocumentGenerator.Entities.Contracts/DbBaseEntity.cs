namespace DocumentGenerator.Entities.Contracts
{
    /// <summary>
    /// Базовая сущность с аудитом
    /// </summary>
    public abstract class DbBaseEntity : IEntityWithId, IEntityWithAudit, IEntityWithSoftDeleted
    {
        /// <inheritdoc cref="IEntityWithId.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="IEntityWithAudit.CreatedAt"/>
        public DateTimeOffset CreatedAt { get; set; }

        /// <inheritdoc cref="IEntityWithAudit.UpdatedAt"/>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <inheritdoc cref="IEntityWithSoftDeleted.DeletedAt"/>
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
