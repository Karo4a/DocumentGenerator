namespace DocumentGenerator.Api.Models.Party
{
    /// <summary>
    /// Модель редактирования стороны акта
    /// </summary>
    public class PartyRequestApiModel
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
