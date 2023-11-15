using Microsoft.AspNetCore.Authentication.JwtBearer;
using TemplateApiNet6.Startup.Swagger;

namespace TemplateApiNet6.Startup.AuthenticationAndAuthorizationOptions;

public static class AuthenticationAndAuthorization
{
    public const string SchemaId = JwtBearerDefaults.AuthenticationScheme;
    public static void AddAuthenticationAndAuthorization(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureDefaultAuthorizationFilterOptions>();
        services.ConfigureOptions<ConfiguredAuthenticationOptions>();
        services.ConfigureOptions<ConfiguredAuthorizationOptions>();
        services.ConfigureOptions<ConfiguredJwtBearerOptions>();
        
        var authBuilder = services.AddAuthentication();
        authBuilder.AddJwtBearer();
        services.AddAuthorization();
    }

    public static void UseAuthenticationAndAuthorization(this IApplicationBuilder appBuilder)
    {
        appBuilder.UseAuthentication();
        appBuilder.UseAuthorization();
    }

    public static TSettings GetCurrent<TSettings>(this IConfiguration IConfiguration)
    {
        return IConfiguration.GetCurrent<TSettings>(nameof(AuthenticationAndAuthorization));
    }
}
