using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemplateApiNet6.Api.Shared;
using TemplateApiNet6.Database;

namespace TemplateApiNet6.Api.v0.OData;

[ApiV0]
[ApiController]
public class PlaylistODataController : BaseODataController<object>
{
    public PlaylistODataController(DbContext DbContext) : base(DbContext) { }
}
