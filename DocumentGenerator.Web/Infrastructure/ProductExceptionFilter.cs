using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Web.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DocumentGenerator.Web.Infrastructure
{
    /// <summary>
    /// Фильтр обработки ошибок
    /// </summary>
    public class ProductExceptionFilter : IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext context)
        {
            if (context.Exception is not ProductException exception)
            {
                return;
            }

            switch (exception)
            {
                case ProductNotFoundException ex:
                    SetDataToContext(new NotFoundObjectResult(new ApiExceptionDetail(ex.Message)), context);
                    break;
                case ProductInvalidOperationException ex:
                    SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail(ex.Message))
                    {
                        StatusCode = StatusCodes.Status406NotAcceptable,
                    }, context);
                    break;
                case ProductValidationException ex:
                    SetDataToContext(new BadRequestObjectResult(new ApiValidationExceptionDetail()
                    {
                        Errors = ex.Errors,
                    })
                    {
                        StatusCode = StatusCodes.Status422UnprocessableEntity,
                    }, context);
                    break;
                default:
                    SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail(exception.Message)), context);
                    break;
            }
        }

        private static void SetDataToContext(ObjectResult data, ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            response.StatusCode = data.StatusCode ?? StatusCodes.Status400BadRequest;
            context.Result = data;
        }
    }
}
