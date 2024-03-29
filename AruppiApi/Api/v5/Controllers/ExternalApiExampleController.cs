using AruppiApi.Api.Shared;
using AruppiApi.Database;
using JikanRest;
using JikanRest.Models;
using Microsoft.AspNetCore.Mvc;
using Riok.Mapperly.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using static JikanRest.Schedules.SchedulesRequestBuilder;

namespace AruppiApi.Api.v5.Controllers.Default;

[ApiV5]
[ApiController]
public partial class ExternalApiExampleController : BaseController<ExternalApiExampleController>
{
    public DatabaseContext DatabaseContext { get; set; }
    public JikanApi JikanApi { get; set; }
    public ExternalApiExampleController(DatabaseContext DatabaseContext, JikanApi JikanApi, IServiceProvider IServiceProvider) : base(IServiceProvider)
    {
        this.DatabaseContext = DatabaseContext;
        this.JikanApi = JikanApi;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "GetSchedulesFilteredByDayOfWeek")]
    public async Task<IEnumerable<ScheduleDto>> Get([FromQuery] SchedulesRequestBuilderGetQueryParameters request, CancellationToken cancellationToken = default)
    {
        var response = await JikanApi.Schedules.GetAsync((b) =>
        {
            b.QueryParameters = request;
        }, cancellationToken);

        var slim = response.Data.Select(i => i.ToScheduleDto());

        return slim;
    }
}

[Mapper]
public static partial class ScheduleMapper
{
    [MapProperty(nameof(@Anime.Images.Webp.LargeImageUrl), nameof(@ScheduleDto.Image))]
    public static partial ScheduleDto ToScheduleDto(this Anime src);
}

public class ScheduleDto
{
    public long MalId { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
}