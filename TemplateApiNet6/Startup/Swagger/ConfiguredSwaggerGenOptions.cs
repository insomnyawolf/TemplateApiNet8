﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System.Reflection;
using TemplateApiNet6.Startup.AuthenticationAndAuthorizationOptions;

namespace TemplateApiNet6.Startup.Swagger;

public class ConfiguredSwaggerGenOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly Generation SwaggerGen;
    private readonly IServer IServer;

    public ConfiguredSwaggerGenOptions(IConfiguration IConfiguration, IServer IServer)
    {
        SwaggerGen = IConfiguration.GetCurrent<Generation>();
        this.IServer = IServer;
    }

    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        options.EnableAnnotations();

        options.AddSecurityDefinition(AuthenticationAndAuthorization.SchemaId, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(SwaggerGen.AuthorizationUrl),
                    Scopes = SwaggerGen.ApiScopes,
                }
            }
        });

        var serverAdresses = IServer.Features.Get<IServerAddressesFeature>();

        if (serverAdresses is not null)
        {
            var count = 1;
            foreach (var currentListeningAddress in serverAdresses.Addresses)
            {
                var server = new OpenApiServer()
                {
                    Description = $"CurrentExecution Address{count}",
                    Url = currentListeningAddress,
                };

                count++;

                options.AddServer(server);
            }
        }

        if (SwaggerGen?.ExtraServers is not null)
        {
            foreach (var server in SwaggerGen.ExtraServers)
            {
                options.AddServer(server);
            }
        }

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    }
}

public class Generation
{
    public string AuthorizationUrl { get; set; }
    public Dictionary<string, string> ApiScopes { get; set; }
    public List<OpenApiServer> ExtraServers { get; set; }
}