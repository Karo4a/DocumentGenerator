namespace DocumentGenerator.Web.Models.Party
{
    /// <summary>
    /// Модель редактирования стороны акта
    /// </summary>
    /// <param name="Name">Наименование</param>
    /// <param name="Job">Должность</param>
    /// <param name="TaxId">ИНН</param>
    public record PartyRequestApiModel(string Name, string Job, string TaxId);
}
