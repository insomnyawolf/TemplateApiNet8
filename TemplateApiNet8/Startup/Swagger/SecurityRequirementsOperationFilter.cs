using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TemplateApiNet8.Startup.AuthenticationAndAuthorizationOptions;

namespace TemplateApiNet8.Startup.Swagger;

public class SecurityRequirementsOperationFilter : IOperationFilter
{
    private readonly Generation SwaggerGen;
    private readonly List<SecurityConfig>? SecurityConfigs;
    public SecurityRequirementsOperationFilter(IConfiguration IConfiguration)
    {
        SwaggerGen = IConfiguration.GetCurrent<Generation>();
        SecurityConfigs = SwaggerGen.SecurityConfigs;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var policyName = new List<string>();

        var metadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;

        foreach (var item in metadata)
        {
            if (item is AuthorizeAttribute auth)
            {
                if (auth.Policy is null)
                {
                    continue;
                }
                policyName.Add(auth.Policy);
            }
        }

        var filter = context.ApiDescription.ActionDescriptor.FilterDescriptors;

        foreach (var item in filter)
        {
            if (item.Filter is AuthorizeFilter auth)
            {
                if (auth.AuthorizeData is null)
                {
                    continue;
                }

                foreach (var data in auth.AuthorizeData)
                {
                    if (data.Policy is null)
                    {
                        continue;
                    }
                    policyName.Add(data.Policy);
                }
            }
        }

        if (policyName.Count < 1)
        {
            return;
        }

        var responses = operation.Responses;

        responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
        responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
        // Not requiered, in theory it should never happen
        // responses.Add("500", new OpenApiResponse { Description = "Internal Server Error" });

        var requirement = new OpenApiSecurityRequirement();

        var policiesString = string.Join(", ", policyName);

        var activeAuthId = AuthenticationAndAuthorization.SchemaId;

        var schema = new OpenApiSecurityScheme
        {
            Description = $"Requiered Autorizations => {policiesString}",
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = activeAuthId,
            }
        };

#warning maybe rework this to
        var active = SecurityConfigs?.SingleOrDefault(item => item.Name == activeAuthId);

        if (active?.SecuritySchemeType == SecuritySchemeType.OAuth2)
        {
            var scopes = active.Oauth2!.ApiScopes!.Values.ToList();

            requirement.Add(schema, scopes);
        }

        var security = operation.Security;
        security.Add(requirement);
    }
}