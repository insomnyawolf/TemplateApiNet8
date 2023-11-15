using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemplateApiNet6.Api.Shared;
using TemplateApiNet6.Database;
using TemplateApiNet6.Database.Models;

namespace TemplateApiNet6.Api.v1.OData;

[ApiV1]
[ApiController]
public class PlaylistODataController : BaseODataController<Playlist>
{
    public PlaylistODataController(DbContext DbContext) : base(DbContext) { }
}
