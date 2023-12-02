using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace TemplateApiNet8.Startup.AuthenticationAndAuthorizationOptions;

public class ConfiguredAuthenticationOptions : IConfigureNamedOptions<AuthenticationOptions>
{
    public void Configure(string? name, AuthenticationOptions options)
    {
        Configure(options);
    }

    public void Configure(AuthenticationOptions options)
    {
        options.DefaultScheme = AuthenticationAndAuthorization.SchemaId;
    }
}
