using DocumentGenerator.Common.Contracts;

namespace DocumentGenerator.Common
{
    /// <inheritdoc cref="IVatRateProvider" />
    public class VatRateProvider : IVatRateProvider
    {
        private const decimal VatRate = 0.2m;

        decimal IVatRateProvider.GetVatRate() => VatRate;
    }
}
