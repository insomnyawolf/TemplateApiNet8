using Microsoft.EntityFrameworkCore;
using TemplateApiNet6.Database;
using TemplateApiNet6.Database.Infraestructure;

namespace TemplateApiNet6.Startup;

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

        services.AddTransient<DatabaseContextConfigurationFilter>();
    }
}
