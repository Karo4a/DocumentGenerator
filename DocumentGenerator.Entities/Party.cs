using DocumentGenerator.Entities.Contracts;

namespace DocumentGenerator.Entities
{
    /// <summary>
    /// Сущность стороны акта
    /// </summary>
    public class Party : DbBaseEntity
    {
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
    }
}
