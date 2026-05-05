using DocumentGenerator.Entities.Contracts;

namespace DocumentGenerator.Entities
{
    /// <summary>
    /// Сущность товара
    /// </summary>
    public class Product : DbBaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Актуальная цена
        /// </summary>
        public decimal Cost { get; set; }
    }
}
