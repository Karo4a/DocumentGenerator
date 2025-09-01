namespace DocumentGenerator.Web.Models
{
    /// <summary>
    /// Модель редактирования товара
    /// </summary>
    /// <param name="Name">Наименование</param>
    /// <param name="Description">Описание</param>
    public record ProductRequestApiModel(string Name, string Description);
}
