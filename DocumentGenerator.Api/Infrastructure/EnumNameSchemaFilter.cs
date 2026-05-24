using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DocumentGenerator.Api.Infrastructure;

/// <summary>
/// Добавляет x-enumNames в OpenAPI-схему для enum-типов.
/// NSwag использует x-enumNames для генерации правильных имён членов перечисления.
/// </summary>
public class EnumNameSchemaFilter : ISchemaFilter
{
    /// <summary>
    /// Применяет фильтр к указанной схеме
    /// </summary>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum) return;

        var names = Enum.GetNames(context.Type);
        var enumNames = new OpenApiArray();
        enumNames.AddRange(names.Select(n => new OpenApiString(n)));

        schema.Extensions["x-enumNames"] = enumNames;
    }
}
