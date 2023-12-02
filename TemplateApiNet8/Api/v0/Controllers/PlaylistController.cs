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
public class PlaylistController : BaseController<PlaylistController>
{
    public DatabaseContext DatabaseContext { get; set; }
    public PlaylistController(DatabaseContext DatabaseContext, IServiceProvider IServiceProvider) : base(IServiceProvider)
    {
        this.DatabaseContext = DatabaseContext;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "GetShowList", Description = "Sample Description")]
    public IQueryable<Show> Get(string? showName = null)
    {
        var playlist = DatabaseContext.Shows.AsQueryable();

        if (!string.IsNullOrEmpty(showName))
        {
            playlist = playlist.Where(item => item.Name == showName);
        }

        return playlist;
    }

    [HttpPost("update")]
    [SwaggerOperation(Summary = "UpdateAvailableShows", Description = "Sample Description")]
    public async Task Update(int startingIdInclusive = 0, int endingIdExclusive = 250, CancellationToken cancellationToken = default)
    {
        var apiClient = IServiceProvider.GetRequiredService<TvMazeApiClient>();

        for (int showId = startingIdInclusive; showId < endingIdExclusive; showId++)
        {
            var show = await apiClient.GetShow(showId, cancellationToken);


        }
    }
}
