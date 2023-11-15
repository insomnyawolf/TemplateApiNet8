using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace TemplateApiNet6.Startup.AuthenticationAndAuthorizationOptions;

public class ConfiguredJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions JwtOptions;

    public ConfiguredJwtBearerOptions(IConfiguration IConfiguration)
    {
        JwtOptions = IConfiguration.GetCurrent<JwtOptions>();
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }

    public void Configure(JwtBearerOptions options)
    {
        options.RequireHttpsMetadata = false;
        options.Authority = JwtOptions.Authority;
        options.TokenValidationParameters.ValidAudiences = JwtOptions.ValidAudiences;
    }
}

public class JwtOptions
{
    public string Authority { get; set; }
    public List<string> ValidAudiences { get; set; }
}
