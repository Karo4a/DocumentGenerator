namespace DocumentGenerator.Services.Contracts.Models.Party
{
    /// <summary>
    /// Модель создания стороны акта
    /// </summary>
    public class PartyCreateModel
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
