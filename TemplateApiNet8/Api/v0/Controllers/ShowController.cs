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
[AllowAnonymous]
public class ShowController : BaseController<ShowController>
{
    public DatabaseContext DatabaseContext { get; set; }
    public ShowController(DatabaseContext DatabaseContext, IServiceProvider IServiceProvider, IOptions<JsonOptions> test) : base(IServiceProvider)
    {
        this.DatabaseContext = DatabaseContext;
    }

    private IQueryable<Database.Models.Show> GetQueryableWithIncludes()
    {
        var dbShows = DatabaseContext.Shows.AsQueryable();

        dbShows = dbShows.Include(i => i.ShowGeneres)
            .ThenInclude(i => i.Genere);

        dbShows = dbShows.Include(i => i.ShowSchedules)
            .ThenInclude(i => i.ScheduleDays)
            .ThenInclude(i => i.Day);

        dbShows = dbShows.Include(i => i.ShowRatings);

        dbShows = dbShows.Include(i => i.ShowNetworks)
            .ThenInclude(i => i.Network)
            .ThenInclude(i => i.CountryNetworks)
            .ThenInclude(i => i.Country);

        dbShows = dbShows.Include(i => i.ShowExternals)
            .ThenInclude(i => i.External);

        //dbShows = dbShows.Include(i => i.S)
        //    .ThenInclude(i => i.External);

        return dbShows;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "GetShowList", Description = "Sample Description")]
    public async Task<IEnumerable<Database.Models.Show>> Get(string? showName = null, CancellationToken cancellationToken = default)
    {
        var dbShows = GetQueryableWithIncludes();

        if (!string.IsNullOrEmpty(showName))
        {
            dbShows = dbShows.Where(item => item.Name == showName);
        }

        var data = await dbShows.ToListAsync();

        //var option = new JsonSerializerOptions()
        //{
        //    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
        //};

        //var str = JsonSerializer.Serialize(data, option);

        return data;
    }

    [HttpGet("single")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "GetShowList", Description = "Sample Description")]
    public async Task<Database.Models.Show> GetSingle(string? showName = null, CancellationToken cancellationToken = default)
    {
        var data = await Get(showName, cancellationToken);
        var f = data.First();
        return f;
    }

    [HttpPost("update")]
    [SwaggerOperation(Summary = "UpdateAvailableShows", Description = "Sample Description")]
    public async Task Update(int startingIdInclusive = 1, int endingIdExclusive = 6, CancellationToken cancellationToken = default)
    {
        var apiClient = IServiceProvider.GetRequiredService<TvMazeApiClient>();

        for (int showId = startingIdInclusive; showId < endingIdExclusive; showId++)
        {
            var apiShow = await apiClient.GetShow(showId, cancellationToken);

            var dbShows = GetQueryableWithIncludes();

            var dbShow = await dbShows.SingleOrDefaultAsync(dbi => dbi.Name == apiShow.Name, cancellationToken);

            if (dbShow is null)
            {
                dbShow = new Database.Models.Show()
                {
                    Id = Guid.NewGuid(),
                };
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

            await UpdateGeneres(dbShow, apiShow, cancellationToken);

            await UpdateSchedules(dbShow, apiShow, cancellationToken);

            #region NewReg
            #endregion NewReg
        }

        await DatabaseContext.SaveChangesAsync();
    }

    private async Task UpdateGeneres(Database.Models.Show dbShow, TvMazeClient.Models.Show apiShow, CancellationToken cancellationToken)
    {
        var dbGeneres = DatabaseContext.Generes.AsQueryable();

        dbGeneres = dbGeneres.Where(db => apiShow.Genres.Any(g => g == db.Name));

        var dbGeneresList = await dbGeneres.ToListAsync(cancellationToken);

        var generesNotIndatabase = apiShow.Genres.Where(g => !dbGeneresList.Any(db => g == db.Name));

        foreach (var genere in generesNotIndatabase)
        {
            var newGenere = new Genere()
            {
                Id = Guid.NewGuid(),
                Name = genere,
            };
            DatabaseContext.Add(newGenere);
            dbGeneresList.Add(newGenere);
        }

        var dbCollection = dbShow.ShowGeneres;

        var delta = dbCollection.FindChanges(dbGeneresList, i => i.GenereId, i => i.Id);


        foreach (var genere in delta.Deleted)
        {
            dbCollection.Remove(genere);
        }

        foreach (var genere in delta.Added)
        {
            dbCollection.Add(new ShowGenere()
            {
                Id = Guid.NewGuid(),
                GenereId = genere.Id,
                ShowId = dbShow.Id,
            });
        }
    }

    private async Task UpdateSchedules(Database.Models.Show dbShow, TvMazeClient.Models.Show apiShow, CancellationToken cancellationToken)
    {
        var dbSchedules = DatabaseContext.ShowSchedules.AsQueryable();

        var apiSchedule = apiShow.Schedule;

        dbSchedules = dbSchedules.Where(db => apiSchedule.Time == db.Time && db.ShowId == dbShow.Id);

        var dbSchedule = await dbSchedules.SingleOrDefaultAsync(cancellationToken);

        if (dbSchedule is null)
        {
            dbSchedule = new Database.Models.ShowSchedule()
            {
                Id = Guid.NewGuid(),
                ShowId = dbShow.Id,
                Time = apiSchedule.Time,
            };
            DatabaseContext.Add(dbSchedule);
        }

        var dbDays = DatabaseContext.Days.AsQueryable();

        dbDays = dbDays.Where(db => apiSchedule.Days.Any(i => i == db.Name));

        var dbDaysList = await dbDays.ToListAsync(cancellationToken);

        var daysNotInDatabase = apiSchedule.Days.Where(g => !dbDaysList.Any(db => g == db.Name));

        foreach (var day in daysNotInDatabase)
        {
            var newGenere = new Day()
            {
                Id = Guid.NewGuid(),
                Name = day,
            };
            DatabaseContext.Add(newGenere);
            dbDaysList.Add(newGenere);
        }

        var dbCollection = dbSchedule.ScheduleDays;

        var delta = dbCollection.FindChanges(dbDaysList, i => i.DayId, i => i.Id);

        // Can schedules be deleted?
        //foreach (var day in delta.Deleted)
        //{
        //    dbCollection.Remove(day);
        //}

        foreach (var day in delta.Added)
        {
            dbCollection.Add(new ScheduleDay()
            {
                Id = Guid.NewGuid(),
                ShowScheduleId = dbSchedule.Id,
                DayId = day.Id,
            });
        }
    }
}
