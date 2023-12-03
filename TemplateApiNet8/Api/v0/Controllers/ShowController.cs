using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApiNet8.Api.Shared;
using TemplateApiNet8.Database;
using TemplateApiNet8.Database.Models;
using TemplateApiNet8.Startup.AuthenticationAndAuthorizationOptions;
using TvMazeClient;

namespace TemplateApiNet8.Api.v0.Controllers.Default;

[ApiV0]
[ApiController]
[AllowAnonymous]
public class ShowController : BaseController<ShowController>
{
    public DatabaseContext DatabaseContext { get; set; }
    public ShowController(DatabaseContext DatabaseContext, IServiceProvider IServiceProvider) : base(IServiceProvider)
    {
        this.DatabaseContext = DatabaseContext;
    }

    private IQueryable<Show> GetQueryableWithIncludes()
    {
        var dbShows = DatabaseContext.Shows.AsQueryable();

        dbShows = dbShows.Include(i => i.ShowGeneres)
            .ThenInclude(i => i.Genere);

        dbShows = dbShows.Include(i => i.Schedules)
            .ThenInclude(i => i.ScheduleDays)
            .ThenInclude(i => i.Day);

        dbShows = dbShows.Include(i => i.Ratings);

        dbShows = dbShows.Include(i => i.ShowNetworks)
            .ThenInclude(i => i.Network)
            .ThenInclude(i => i.CountryNetworks)
            .ThenInclude(i => i.Country);

        //dbShows = dbShows.Include(i => i.)
        //    .ThenInclude(i => i.Network)
        //    .ThenInclude(i => i.CountryNetworks)
        //    .ThenInclude(i => i.Country);

        return dbShows;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "GetShowList", Description = "Sample Description")]
    public IQueryable<Show> Get(string? showName = null)
    {
        var dbShows = GetQueryableWithIncludes();

        if (!string.IsNullOrEmpty(showName))
        {
            dbShows = dbShows.Where(item => item.Name == showName);
        }

        return dbShows;
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
                dbShow = new Show()
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

            var dbGeneres = DatabaseContext.Generes.AsQueryable();

            dbGeneres = dbGeneres.Where(dbg => apiShow.Genres.Any(g => g == dbg.Name));

            var dbGeneresList = await dbGeneres.ToListAsync(cancellationToken);

            var generesNotIndatabase = apiShow.Genres.Where(g => !dbGeneresList.Any(dg => g == dg.Name));

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

            var delta = FindChanges(dbShow.ShowGeneres, dbGeneresList, i => i.GenereId, i => i.Id);

            foreach (var genere in delta.Deleted)
            {
                dbShow.ShowGeneres.Remove(genere);
            }

            foreach (var genere in delta.Added)
            {
                dbShow.ShowGeneres.Add(new ShowGenere()
                {
                    Id = Guid.NewGuid(),
                    GenereId = genere.Id,
                    ShowId = dbShow.Id,
                });
            }
        }

        await DatabaseContext.SaveChangesAsync();
    }

    private static (IEnumerable<Y> Added, IEnumerable<T> Deleted) FindChanges<T, Y, Z>(ICollection<T> baseList, List<Y> newList, Func<T, Z> baseSelector, Func<Y, Z> newSelector) where Z : IEquatable<Z>
    {
        var comparer = EqualityComparer<Z>.Default;
        var newGeneres = newList.Where(@new => !baseList.Any(bas => comparer.Equals(newSelector(@new), baseSelector(bas))));
        var deletedGeneres = baseList.Where(bas => !newList.Any(@new => comparer.Equals(baseSelector(bas), newSelector(@new))));
        return (newGeneres, deletedGeneres);
    }
}
