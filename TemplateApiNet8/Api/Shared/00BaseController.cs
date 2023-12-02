using Microsoft.AspNetCore.Mvc;
using TemplateApiNet8.Database.Infraestructure;

namespace TemplateApiNet8.Api.Shared;

// "Light Controller" May not extend Controller base
// Check Performance Implications
//
// services.AddHttpContextAccessor();
// var ctx = IServiceProvider.GetRequiredService<IHttpContextAccessor>();

[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController<TController> : ControllerBase where TController : BaseController<TController>
{
    protected readonly ILogger<TController> Logger;

    public BaseController(IServiceProvider IServiceProvider)
    {
        Logger = IServiceProvider.GetRequiredService<ILogger<TController>>();
    }
}
