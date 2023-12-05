using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;
using System.Threading;
using TemplateApiNet8.Api.Shared;
using TemplateApiNet8.Database;
using TemplateApiNet8.Database.Models;
using TemplateApiNet8.Extensions;
using TemplateApiNet8.Startup.AuthenticationAndAuthorizationOptions;
using TvMazeClient;
using TvMazeClient.Models;

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
