using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TemplateApiNet8.Startup.OData;

namespace TemplateApiNet8.Startup;

public static class Json
{
    public static void ConfigureJson(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureJsonOptions>();
    }
}

public class ConfigureJsonOptions : IConfigureNamedOptions<JsonOptions>
{
    public void Configure(string? name, JsonOptions options)
    {
        Configure(options);
    }

    public void Configure(JsonOptions options)
    {
        var so = options.JsonSerializerOptions;
        // Sane Defaults
        so.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        so.MaxDepth = 32;
    }
}
public sealed class CustomIgnoreReferenceHandler : ReferenceHandler
{
    public CustomIgnoreReferenceHandler()
    {

    }

    public override ReferenceResolver CreateResolver() => new CustomIgnoreReferenceResolver();
}

public sealed class CustomIgnoreReferenceResolver : ReferenceResolver
{
    public override void AddReference(string referenceId, object value)
    {
        throw new NotImplementedException();
    }

    public override string GetReference(object value, out bool alreadyExists)
    {
        throw new NotImplementedException();
    }

    public override object ResolveReference(string referenceId)
    {
        throw new NotImplementedException();
    }
}