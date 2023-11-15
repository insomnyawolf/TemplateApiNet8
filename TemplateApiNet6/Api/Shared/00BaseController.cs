using Microsoft.AspNetCore.Mvc;
using TemplateApiNet6.Database.Infraestructure;

namespace TemplateApiNet6.Api.Shared;

// "Light Controller" May not extend Controller base
// Check Performance Implications
//
// services.AddHttpContextAccessor();
// var ctx = IServiceProvider.GetRequiredService<IHttpContextAccessor>();

// This won't work
// https://stackoverflow.com/questions/72935856/net-6-concatenate-routes-of-base-classes
// [Route("api/v{version:apiVersion}")]
[Route("api/v{version:apiVersion}/[controller]")]
[ServiceFilter(typeof(DatabaseContextConfigurationFilter))]
public abstract class BaseController<TController> : ControllerBase where TController : BaseController<TController>
{
    protected readonly ILogger<TController> Logger;

    public BaseController(IServiceProvider IServiceProvider)
    {
        Logger = IServiceProvider.GetRequiredService<ILogger<TController>>();
    }
}
