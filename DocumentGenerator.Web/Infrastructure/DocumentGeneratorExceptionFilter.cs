using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Web.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DocumentGenerator.Web.Infrastructure
{
    /// <summary>
    /// Фильтр обработки ошибок
    /// </summary>
    public class DocumentGeneratorExceptionFilter : IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext context)
        {
            if (context.Exception is not DocumentGeneratorException exception)
            {
                return;
            }

            switch (exception)
            {
                case DocumentGeneratorNotFoundException ex:
                    SetDataToContext(new NotFoundObjectResult(new ApiExceptionDetail(ex.Message)), context);
                    break;
                case DocumentGeneratorInvalidOperationException ex:
                    SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail(ex.Message))
                    {
                        StatusCode = StatusCodes.Status406NotAcceptable,
                    }, context);
                    break;
                case DocumentGeneratorValidationException ex:
                    SetDataToContext(new BadRequestObjectResult(new ApiValidationExceptionDetail()
                    {
                        Errors = ex.Errors,
                    })
                    {
                        StatusCode = StatusCodes.Status422UnprocessableEntity,
                    }, context);
                    break;
                case DocumentGeneratorDuplicateException ex:
                    SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail(ex.Message))
                    {
                        StatusCode = StatusCodes.Status409Conflict,
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
