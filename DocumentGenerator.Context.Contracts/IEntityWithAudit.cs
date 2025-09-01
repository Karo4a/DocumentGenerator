namespace DocumentGenerator.Context.Contracts
{
    /// <summary>
    /// Интерфейс сущности с аудитом
    /// </summary>
    public interface IEntityWithAudit
    {
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
