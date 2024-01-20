using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolrNet;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApiNet8.Api.Shared;
using TemplateApiNet8.Database;
using TemplateApiNet8.Database.Models;
using ApiGetGenerator;

namespace TemplateApiNet8.Api.v0.Controllers.Default;

[ApiV0]
[ApiController]
public partial class TestController : BaseController<ShowController>
{
    public DatabaseContext DatabaseContext { get; set; }
    public ISolrOperations<TestClass> SolrClient { get; set; }
    public TestController(DatabaseContext DatabaseContext, IServiceProvider IServiceProvider) : base(IServiceProvider)
    {
        this.DatabaseContext = DatabaseContext;
    }

    [HttpGet("AltGet")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "GetShowList", Description = "Sample Description")]
    [GenerateSolrFilterAttribute(InyectedSolrClientName = nameof(ShowController.SolrClient))]
    public partial Task<Page<TestClass>> AutoGet([FromQuery] TestClassQuery? query = null);

    [HttpGet("AltGet2")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "GetShowList", Description = "Sample Description")]
    [GenerateEntityFrameworkFilterAttribute(InyectedDatabaseContextName = nameof(ShowController.DatabaseContext))]
    public partial Task<Page<TestClass>> AutoGet2([FromQuery] TestClassQuery? query = null);

    [HttpGet("AltGet3")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "GetShowList", Description = "Sample Description")]
    [GenerateEntityFrameworkFilterAttribute(InyectedDatabaseContextName = nameof(ShowController.DatabaseContext))]
    public partial Task<Page<TestClass>> AutoGet3([FromQuery] TestClassQuery? query = null);

    [HttpGet("AltGet4")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "GetShowList", Description = "Sample Description")]
    [GenerateEntityFrameworkFilterAttribute(InyectedDatabaseContextName = nameof(ShowController.DatabaseContext))]
    public partial Task<Page<Genere>> AutoGet453253([FromQuery] GenereQuery? query = null);
}

public partial class TestClass : BaseEntity
{
    public TestClass? TestClasss { get; set; }
    public byte[]? Datetime { get; set; }
    public override Guid Id { get; set; }
    public bool? OnEmision { get; set; }
    public string? Name { get; set; }
    public int? Runtime { get; set; }
    public DateTimeOffset? Premiered { get; set; }
    public IList<ScheduleDay>? Schedules { get; set; }
}