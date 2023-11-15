﻿using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace TemplateApiNet6.Startup.Swagger;

public static class Swagger
{
    public static void AddSwaggerConfigured(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfiguredSwaggerUIOptions>(); 
        services.ConfigureOptions<ConfiguredSwaggerGenOptions>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
    }

    public static void UseSwaggerConfigured(this IApplicationBuilder appBuilder)
    {
        appBuilder.UseSwagger();
        appBuilder.UseSwaggerUI();
    }

    public static TSettings GetCurrent<TSettings>(this IConfiguration IConfiguration)
    {
        return IConfiguration.GetCurrent<TSettings>(nameof(Swagger));
    }
}
