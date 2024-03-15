using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AruppiApi.Api.Shared;
using AruppiApi.Database;
using Riok.Mapperly.Abstractions;
using JikanRest.Models;
using JikanRest;
using static JikanRest.Schedules.SchedulesRequestBuilder;
using JikanRest.Schedules;
using AruppiApi.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AruppiApi.Api.v5.Controllers.Default;

[ApiV5]
[ApiController]
public partial class DatabaseExampleController : BaseController<ExternalApiExampleController>
{
    public DatabaseContext DatabaseContext { get; set; }
    public DatabaseExampleController(DatabaseContext DatabaseContext, IServiceProvider IServiceProvider) : base(IServiceProvider)
    {
        this.DatabaseContext = DatabaseContext;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "GetsTrackById(if provided)")]
    public async Task<IEnumerable<Track>> Get(int? id, CancellationToken cancellationToken = default)
    {
        // From Example
        var query = DatabaseContext.Tracks.AsQueryable();

        // Join Example
        query = query.Include(item => item.Genre);

        // Limits the amount of results
        query = query.Take(10);

        if (id is not null)
        {
            // Where Example
            query = query.Where(item => item.TrackId == id);
        }

        return query;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Adds New Track")]
    public async Task Post(Track track)
    {
        DatabaseContext.Tracks.Add(track);

        await DatabaseContext.SaveChangesAsync();
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Edits Existing Track")]
    public async Task<IEnumerable<Track>> Put(Track track)
    {
        var query = DatabaseContext.Tracks.AsQueryable();

        query = query.Where(item => item.TrackId == track.TrackId);

        var dbItem = query.FirstOrDefault();

        if (dbItem is null)
        {
            throw new Exception("Can not edit item that doesn't exist");
        }

        dbItem.AlbumId = track.AlbumId;
        dbItem.Composer = track.Composer;
        // ...

        await DatabaseContext.SaveChangesAsync();

        return query;
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "DeletesTrackById")]
    public async Task Delete(int id)
    {
        var query = DatabaseContext.Tracks.AsQueryable();

        query = query.Where(item => item.TrackId == id);

        var item = query.FirstOrDefaultAsync();

        DatabaseContext.Remove(item);

        await DatabaseContext.SaveChangesAsync();
    }
}