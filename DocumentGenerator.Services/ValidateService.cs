using DocumentGenerator.Services.Contracts;
using DocumentGenerator.Services.Validators;
using DocumentGenerator.Services.Contracts.Exceptions;
using FluentValidation;
using DocumentGenerator.Services.Contracts.Models;

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
        }

        async Task IValidateService.Validate<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            if (!validators.TryGetValue(model.GetType(), out var validator))
            {
                throw new ProductInvalidOperationException($"Не найден запрашиваемый валидатор {model.GetType()}");
            }

            var context = new ValidationContext<TModel>(model);
            var validationResult = await validator.ValidateAsync(context, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ProductValidationException(validationResult.Errors.Select(x => InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
