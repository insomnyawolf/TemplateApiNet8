using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApiNet8.Api.Shared;
using TemplateApiNet8.Database;
using TemplateApiNet8.Startup.AuthenticationAndAuthorizationOptions;
using TemplateApiNet8.Database.Models;

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

    //[HttpGet]
    //[SwaggerOperation(Summary = "Sample Summary", Description = "Sample Description")]
    //public IQueryable<Show> Get(int? PlaylistId = null)
    //{
    //    var playlist = DatabaseContext.Playlists.AsQueryable();

    //    if (PlaylistId.HasValue)
    //    {
    //        playlist = playlist.Where(item => item.PlaylistId == PlaylistId);
    //    }

    //    return playlist;
    //}
}
