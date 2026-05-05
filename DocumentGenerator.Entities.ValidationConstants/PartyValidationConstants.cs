namespace DocumentGenerator.Entities.ValidationConstants
{
    /// <summary>
    /// Класс констант валидации <see cref="Party"/>
    /// </summary>
    public class PartyValidationConstants
    {
        /// <summary>
        /// Максимальная длина <see cref="Party.Name"/>
        /// </summary>
        public const int NameMaxLength = 255;

        /// <summary>
        /// Минимальная длина <see cref="Party.Name"/>
        /// </summary>
        public const int NameMinLength = 3;

        /// <summary>
        /// Максимальная длина <see cref="Party.Job"/>
        /// </summary>
        public const int JobMaxLength = 255;

        /// <summary>
        /// Длина <see cref="Party.TaxId"/> для юридических лиц
        /// </summary>
        public const int LegalTaxIdLength = 12;

        /// <summary>
        /// Длина <see cref="Party.TaxId"/> для физических лиц
        /// </summary>
        public const int IndividualTaxIdLength = 10;

        /// <summary>
        /// Максимальная длина <see cref="Party.TaxId"/>
        /// </summary>
        public const int TaxIdMaxLength = 12;
    }
}
