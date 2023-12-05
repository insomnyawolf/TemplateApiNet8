using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Options;

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