using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TemplateApiNet6.Startup.AuthenticationAndAuthorizationOptions;

namespace TemplateApiNet6.Startup.Swagger;

public class SecurityRequirementsOperationFilter : IOperationFilter
{
    private readonly Generation SwaggerGen;
    private readonly List<string> Scopes;
    public SecurityRequirementsOperationFilter(IConfiguration IConfiguration)
    {
        SwaggerGen = IConfiguration.GetCurrent<Generation>();
        Scopes = SwaggerGen.ApiScopes.Values.ToList();
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var policyName = new List<string>();

        var metadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;

        foreach (var item in metadata)
        {
            if (item is AuthorizeAttribute auth)
            {
                policyName.Add(auth.Policy);
            }
        }

        var filter = context.ApiDescription.ActionDescriptor.FilterDescriptors;

        foreach (var item in filter)
        {
            if (item.Filter is AuthorizeFilter auth)
            {
                foreach (var data in auth.AuthorizeData)
                {
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

        var requirement = new OpenApiSecurityRequirement();

        var policiesString = string.Join(", ", policyName);

        var schema = new OpenApiSecurityScheme
        {
            Description = $"Requiered Autorizations => {policiesString}",
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = AuthenticationAndAuthorization.SchemaId,
            }
        };

        requirement.Add(schema, Scopes);

        var security = operation.Security;
        security.Add(requirement);
    }
}