using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApiNet8.Api.Shared;
using TemplateApiNet8.Database;
using TemplateApiNet8.Startup.AuthenticationAndAuthorizationOptions;
using TemplateApiNet8.Database.Models;
using TvMazeClient;

namespace TemplateApiNet8.Api.v0.Controllers.Default;

[ApiV0]
[ApiController]
public class ShowController : BaseController<ShowController>
{
    public DatabaseContext DatabaseContext { get; set; }
    public ShowController(DatabaseContext DatabaseContext, IServiceProvider IServiceProvider) : base(IServiceProvider)
    {
        this.DatabaseContext = DatabaseContext;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "GetShowList", Description = "Sample Description")]
    public IQueryable<Show> Get(string? showName = null)
    {
        var dbShows = DatabaseContext.Shows.AsQueryable();

        if (!string.IsNullOrEmpty(showName))
        {
            dbShows = dbShows.Where(item => item.Name == showName);
        }

        return dbShows;
    }

    [HttpPost("update")]
    [SwaggerOperation(Summary = "UpdateAvailableShows", Description = "Sample Description")]
    public async Task Update(int startingIdInclusive = 0, int endingIdExclusive = 250, CancellationToken cancellationToken = default)
    {
        var apiClient = IServiceProvider.GetRequiredService<TvMazeApiClient>();

        for (int showId = startingIdInclusive; showId < endingIdExclusive; showId++)
        {
            var apiShow = await apiClient.GetShow(showId, cancellationToken);

            var dbShows = DatabaseContext.Shows.AsQueryable();

            var dbShow = dbShows.SingleOrDefault(dbi => dbi.Name == apiShow.Name);

            if (dbShow is null)
            {
                dbShow = new Show();
                DatabaseContext.Add(dbShow);
            }

            dbShow.Url = apiShow.Url;
            dbShow.Name = apiShow.Name;
            dbShow.Runtime = apiShow.Runtime;
            dbShow.AverageRuntime = apiShow.AverageRuntime;
            dbShow.Premiered = apiShow.Premiered;
            dbShow.Ended = apiShow.Ended;
            dbShow.OfficialSite = apiShow.OfficialSite;
            dbShow.Weight = apiShow.Weight;
            dbShow.Summary = apiShow.Summary;
            dbShow.Updated = apiShow.Updated;
        }

        await DatabaseContext.SaveChangesAsync();
    }
}
