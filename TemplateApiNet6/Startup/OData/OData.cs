using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Swashbuckle.AspNetCore.SwaggerGen;
using TemplateApiNet6.Startup.Swagger;

namespace TemplateApiNet6.Startup.OData;

public static class OData
{
    // OData by default breaks openapi,
    // it creates dynamic copies of the models used that looks exactly the same but are different
    // that by default creates an id colision in swagger
    public static void AddODataConfigured(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureODataOptions>(); 

        var mvcBuilder = services.AddMvc();
        mvcBuilder.AddOData();
    }
    
    public static void UseODataConfigured(this WebApplication app)
    {
#if DEBUG
        if (app.Environment.IsDevelopment())
        {
            app.UseODataRouteDebug();
        }
#endif
        app.UseODataQueryRequest();
        app.UseODataBatching();
        app.UseVersionedODataBatching();
    }
}