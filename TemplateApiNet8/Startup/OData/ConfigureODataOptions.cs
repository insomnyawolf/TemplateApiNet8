using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TemplateApiNet8.Startup.AuthenticationAndAuthorizationOptions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Collections.Generic;
using Microsoft.AspNetCore.OData;

namespace TemplateApiNet8.Startup.Swagger;

public class ConfigureODataOptions : IConfigureNamedOptions<ODataOptions>
{
    public ConfigureODataOptions() { }

    public void Configure(string? name, ODataOptions options)
    {
        Configure(options);
    }

    public void Configure(ODataOptions options)
    {
        options.EnableQueryFeatures(maxTopValue: 5);
    }
}