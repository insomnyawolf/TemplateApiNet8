using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using TemplateApiNet8.Database;
using TemplateApiNet8.Database.Infraestructure;

namespace TemplateApiNet8.Startup;

public static class Database
{
    public static void AddDatabaseContext(this IServiceCollection services)
    {
        services.AddDbContext<DbContext, DatabaseContext>(optionsAction: (serviceProvider, dbContextOptionsBuilder) =>
        {
#if DEBUG
            dbContextOptionsBuilder.EnableDetailedErrors();
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
#endif
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString(nameof(DatabaseContext));

            // Change this as needed
            dbContextOptionsBuilder.UseSqlite(connectionString);
        });

        services.ConfigureOptions<ConfigureEfCoreJsonOptions>();
    }
}

public class ConfigureEfCoreJsonOptions : IConfigureNamedOptions<JsonOptions>
{
    public void Configure(string? name, JsonOptions options)
    {
        Configure(options);
    }

    public void Configure(JsonOptions options)
    {
        var so = options.JsonSerializerOptions;
        // This allows using includes and returning it's data in EntityFrameworkQueries
        so.TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                DatabaseModelsSerialization.RemoveForeignKeyProperties
            }
        };
    }
}