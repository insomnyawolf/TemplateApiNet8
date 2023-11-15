using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace TemplateApiNet6.Startup.AuthenticationAndAuthorizationOptions;

public class ConfiguredAuthorizationOptions : IConfigureNamedOptions<AuthorizationOptions>
{
    public void Configure(string? name, AuthorizationOptions options)
    {
        Configure(options);
    }

    public void Configure(AuthorizationOptions options)
    {
        var authenticationSchemes = new string[]
        {
            AuthenticationAndAuthorization.SchemaId,
        };

        var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(authenticationSchemes);
        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

        var policy = defaultAuthorizationPolicyBuilder.Build();
        options.DefaultPolicy = policy;
    }
}
