using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApiNet8.Api.Shared;
using TemplateApiNet8.Database;

namespace TemplateApiNet8.Api.v0.Controllers.Default;

[ApiV0]
[ApiController]
public class SampleController : BaseController<ShowController>
{
    public DatabaseContext DatabaseContext { get; set; }
    public SampleController(DatabaseContext DatabaseContext, IServiceProvider IServiceProvider) : base(IServiceProvider)
    {
        this.DatabaseContext = DatabaseContext;
    }

    [HttpGet(nameof(SampleEndpointWithNoConfiguration))]
    [SwaggerOperation(Summary = "UpdateAvailableShows", Description = "Sample Description")]
    public async Task SampleEndpointWithNoConfiguration(CancellationToken cancellationToken = default)
    {
    }

    [Authorize(Policy = "SamplePolicy, Other", Roles = "SampleRole, Others")]
    [HttpGet(nameof(SampleEndpointWithCustomPoliciesAndRoles))]
    [SwaggerOperation(Summary = "UpdateAvailableShows", Description = "Sample Description")]
    public async Task SampleEndpointWithCustomPoliciesAndRoles(CancellationToken cancellationToken = default)
    {
    }
}
