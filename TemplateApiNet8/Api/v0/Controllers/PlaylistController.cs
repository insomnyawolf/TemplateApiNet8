using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApiNet6.Api.Shared;
using TemplateApiNet6.Database;
using TemplateApiNet6.Database.Models;
using TemplateApiNet6.Startup.AuthenticationAndAuthorizationOptions;

namespace TemplateApiNet6.Api.v0.Controllers.Default;

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
    [SwaggerOperation(Summary = "Sample Summary", Description = "Sample Description")]
    public IQueryable<Playlist> Get(int? PlaylistId = null)
    {
        var playlist = DatabaseContext.Playlists.AsQueryable();

        if (PlaylistId.HasValue)
        {
            playlist = playlist.Where(item => item.PlaylistId == PlaylistId);
        }

        return playlist;
    }
}
