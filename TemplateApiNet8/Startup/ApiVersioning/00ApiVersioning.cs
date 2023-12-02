using Asp.Versioning;
using Asp.Versioning.OData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OData.ModelBuilder;
using TemplateApiNet8.Api.v0;

namespace TemplateApiNet8.Startup.ApiVersioning;

// https://github.com/dotnet/aspnet-api-versioning/wiki/
public static class ApiVersioning
{
    public static ApiVersion DefaultApiVersion => ApiV0Attribute.Version;

    public static void AddApiVersioningConfigured(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfiguredApiVersioningOptions>();

        services.ConfigureOptions<VersionedApiExplorerOptions>();
        services.ConfigureOptions<ConfiguredODataApiExplorerOptions>();

        services.ConfigureOptions<VersionedSwaggerGenOptions>();

        var builder = services.AddApiVersioning();

        builder.AddMvc();

        builder.AddApiExplorer();

        builder.AddOData();
        builder.AddODataApiExplorer();
    }
}
