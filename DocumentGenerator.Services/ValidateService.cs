using DocumentGenerator.Services.Contracts;
using DocumentGenerator.Services.Validators;
using DocumentGenerator.Services.Contracts.Exceptions;
using FluentValidation;
using DocumentGenerator.Services.Contracts.Models.Party;
using DocumentGenerator.Services.Contracts.Models.Product;
using DocumentGenerator.Services.Contracts.Models.Document;

namespace DocumentGenerator.Services
{
    /// <inheritdoc cref="IValidateService"/>
    public class ValidateService : IValidateService
    {
        private readonly IDictionary<Type, IValidator> validators;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ValidateService()
        {
            validators = new Dictionary<Type, IValidator>();
            validators.TryAdd(typeof(ProductCreateModel), new ProductCreateModelValidator());
            validators.TryAdd(typeof(PartyCreateModel), new PartyCreateModelValidator());
            validators.TryAdd(typeof(DocumentCreateModel), new DocumentCreateModelValidator());
        }

        async Task IValidateService.Validate<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            if (!validators.TryGetValue(model.GetType(), out var validator))
            {
                throw new InvalidOperationException($"Не найден запрашиваемый валидатор {model.GetType()}");
            }

            var context = new ValidationContext<TModel>(model);
            var validationResult = await validator.ValidateAsync(context, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new DocumentGeneratorValidationException(validationResult.Errors.Select(x => 
                    InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
