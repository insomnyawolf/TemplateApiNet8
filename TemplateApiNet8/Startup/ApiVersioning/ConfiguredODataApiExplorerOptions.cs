using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;

namespace TemplateApiNet6.Startup.ApiVersioning
{
    public class ConfiguredODataApiExplorerOptions : IConfigureNamedOptions<ODataApiExplorerOptions>
    {
        public void Configure(string? name, ODataApiExplorerOptions options)
        {
            Configure(options);
        }

        public void Configure(ODataApiExplorerOptions options)
        {
            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";

            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        }
    }
}
