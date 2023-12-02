using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TemplateApiNet8.Startup;
using TemplateApiNet8.Startup.ApiVersioning;
using TemplateApiNet8.Startup.AuthenticationAndAuthorizationOptions;
using TemplateApiNet8.Startup.HealthCheck;
using TemplateApiNet8.Startup.OData;
using TemplateApiNet8.Startup.Swagger;
using TvMazeClient;

namespace TemplateApiNet8;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add Configuration
        // This initializes the configuration abstractions
        // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0
        var configuration = builder.Configuration;

        // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0#default-application-configuration-sources
        configuration.AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: false);
        configuration.AddJsonFile(path: "appsettings.Development.json", optional: true, reloadOnChange: true);
        configuration.AddEnvironmentVariables();
        configuration.AddCommandLine(args);

        // Add services to the container.
        var services = builder.Services;

        services.AddHealthCheckConfigured();

        // https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
        services.AddHttpClient();

        services.AddDatabaseContext();

        var mvcBuilder = services.AddControllers();

        services.ConfigureJson();

        services.AddODataConfigured();

        services.AddApiVersioningConfigured();

        services.AddAuthenticationAndAuthorization();

        services.AddSwaggerConfigured();

#if DEBUG
        services.AddHttpLogging((options) =>
        {
        });
#endif

        services.AddTransient<TvMazeApiClient>((IServiceProvider) =>
        {
            var httpClientFactory = IServiceProvider.GetRequiredService<IHttpClientFactory>();
            return new TvMazeApiClient(httpClientFactory);
        });

        var app = builder.Build();
        // Configure the HTTP request pipeline.

        app.UseCors(options =>
        {
            options.AllowAnyOrigin();
            options.AllowAnyMethod();
            options.AllowAnyHeader();
        });

        app.UseODataConfigured();

        app.UseAuthenticationAndAuthorization();

        app.UseHealthCheckConfigured();

        app.MapControllers();

#if DEBUG
        if (app.Environment.IsDevelopment())
        {
            app.UseHttpLogging();
            app.UseDeveloperExceptionPage();

            app.UseSwaggerConfigured();
        }
#endif
        
        app.Run();
    }
}
