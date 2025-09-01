namespace DocumentGenerator.Services.Contracts.Models
{
    /// <summary>
    /// Модель создания товара
    /// </summary>
    public class ProductCreateModel
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public ProductCreateModel()
        {

        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        public ProductCreateModel(string name, string description)
        {
            Name = name;
            Description = description;
        }
    };
}
