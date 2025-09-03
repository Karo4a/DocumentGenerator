namespace DocumentGenerator.Services.Contracts.Models.Product
{
    /// <summary>
    /// Модель создания товара
    /// </summary>
    public class ProductCreateModel
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;
    };
}
