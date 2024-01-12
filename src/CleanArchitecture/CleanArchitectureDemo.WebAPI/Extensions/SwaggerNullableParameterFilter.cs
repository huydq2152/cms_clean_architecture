using AutoMapper.Internal;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitectureDemo.WebAPI.Extensions;

public class SwaggerNullableParameterFilter : IParameterFilter
{
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        if (!parameter.Schema.Nullable && (context.ApiParameterDescription.Type.IsNullableType() ||
                                           context.ApiParameterDescription.Type == typeof(string)))
            parameter.Schema.Nullable = true;
    }
}