namespace DocumentGenerator.Entities.Contracts
{
    /// <summary>
    /// Интерфейс сущности с удалением
    /// </summary>
    public interface IEntityWithSoftDeleted
    {
        /// <summary>
        /// Дата удаления записи
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
