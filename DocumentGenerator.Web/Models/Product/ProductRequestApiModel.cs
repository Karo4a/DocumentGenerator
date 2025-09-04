namespace DocumentGenerator.Web.Models.Product
{
    /// <summary>
    /// Модель редактирования товара
    /// </summary>
    /// <param name="Name">Наименование</param>
    /// <param name="Cost">Актуальная цена</param>
    public record ProductRequestApiModel(string Name, decimal Cost);
}
