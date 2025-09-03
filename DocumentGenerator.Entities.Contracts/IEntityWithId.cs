namespace DocumentGenerator.Entities.Contracts
{
    /// <summary>
    /// Интерфейс сущности с идентификатором
    /// </summary>
    public interface IEntityWithId
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        Guid Id { get; set; }
    }
}
