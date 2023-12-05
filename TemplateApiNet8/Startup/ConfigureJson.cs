﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using TemplateApiNet8.Startup.OData;
using System.Runtime.CompilerServices;
using TemplateApiNet8.Database.Models;

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
        so.MaxDepth = 128;
    }
}