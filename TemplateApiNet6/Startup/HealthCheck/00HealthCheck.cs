﻿using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace TemplateApiNet6.Startup.HealthCheck;

// https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-6.0
public static class HealthCheck
{
    public static void AddHealthCheckConfigured(this IServiceCollection services)
    {
        var builder = services.AddHealthChecks();
        builder.AddCheck<DatabaseHealthCheck>(nameof(DatabaseHealthCheck));
    }

    public static void UseHealthCheckConfigured(this WebApplication app)
    {
        var options = new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        };

        // https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-7.0#usehealthchecks-vs-maphealthchecks
        app.UseHealthChecks(path: "/health", options: options);
    }
}
