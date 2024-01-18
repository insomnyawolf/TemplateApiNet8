using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ApiGetGenerator;
using TemplateApiNet8.Database;
using SolrNet;
using TemplateApiNet8.Database.Models;
using Microsoft.AspNetCore.Mvc;
using SolrNet.Commands.Parameters;
using static SolrNet.StartOrCursor;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TemplateApiNet8.Api.v0.Controllers.Default2;

public partial class ShowQuery
{
    public List<OrderBy<ShowTempColumns>> OrderBy { get; set; }
    public List<ShowTempIncludes> Includes { get; set; }
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public Guid? IdMax { get; set; }
    public Guid? IdMin { get; set; }
    public Boolean? OnEmision { get; set; }
    public String? Name { get; set; }
    public StringComparationType? NameComparationType { get; set; }
    public Int32? RuntimeMax { get; set; }
    public Int32? RuntimeMin { get; set; }
    public DateTimeOffset? PremieredMax { get; set; }
    public DateTimeOffset? PremieredMin { get; set; }

}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ShowTempColumns
{
    Id,
    OnEmision,
    Name,
    Runtime,
    Premiered,

}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ShowTempIncludes
{
    Schedules,

}

public static class ShowController2Help
{
    public static string GetValueOrAsterisk<T>(this Nullable<T> item) where T : struct
    {
        return item?.ToString() ?? "*";
    }
}

    public partial class ShowController2
{
    public DatabaseContext DatabaseContext { get; set; }
    public ISolrOperations<ShowTemp> Solr { get; set; }
    public ShowController2(IServiceProvider IServiceProvider)
    {
        this.DatabaseContext = DatabaseContext;
        this.Solr = IServiceProvider.GetRequiredService<ISolrOperations<ShowTemp>>();
    }

    [HttpGet]
    public virtual async Task<Page<ShowTemp>> SolrQuery(ShowQuery? query)
    {
        var pageSize = query?.PageSize ?? 10;
        var pageIndex = query?.PageIndex ?? 0;

        // Pagination settings
        var options = new QueryOptions()
        {
            Fields = new List<string>() { "*", "[child]" },
            Rows = pageSize,
            StartOrCursor = new Start(pageSize * pageIndex),
        };

        if (query is not null)
        {
            var orderBy = query.OrderBy;

            if (orderBy is not null)
            {
                var srt = new List<SortOrder>();

                for (int i = 0; i < orderBy.Count; i++)
                {
                    var current = orderBy[i];

                    var direction = current.Direction switch
                    {
                        OrderDirection.Ascending => Order.ASC,
                        OrderDirection.Descending => Order.DESC,
                    };

                    var fieldName = current.Column.ToString();

                    srt.Add(new SortOrder(fieldName, direction));
                }

                options.OrderBy = srt;
            }

            var fqs = new List<ISolrQuery>();

            if (query.IdMin is not null || query.IdMax is not null)
            {
                var min = query.IdMin.GetValueOrAsterisk();
                var max = query.IdMax.GetValueOrAsterisk();
                var sq = new SolrQuery($"fieldname:[{min} TO {max}]");

                fqs.Add(sq);
            }

            if (query.Name is not null)
            {
                var filter = query.NameComparationType switch
                {
                    StringComparationType.StartsWith => $"fieldname:{query.Name}*",
                    StringComparationType.EndsWith => $"fieldname:*{query.Name}",
                    StringComparationType.Contains => $"fieldname:*{query.Name}*",
                    StringComparationType.Equals => $"fieldname:{query.Name}",
                    _ => $"fieldname:{query.Name}",
                };

                var sq = new SolrQuery(filter);

                fqs.Add(sq);
            }

            if (query.OnEmision is not null)
            {
                var sq = new SolrQuery($"fieldname:{query.OnEmision}");

                fqs.Add(sq);
            }

            options.FilterQueries = fqs;
        }

        // By default if the query is empty it doesn't return anything
        // That's not a bad behaviour but i personally prefer having some data directly so i know if things are working properly
        //var defaultQuery = "*";
        //This should be faster since it needs less string checks
        var defaultQuery = "-_nest_path_:*";

        var q = await Solr.QueryAsync(defaultQuery, options);

        // We use a custom class to return the pagination data because the SolrQueryResults doesn't serialize any extra fields, just the data
        return new(q, (int)q.NumFound, pageSize, pageIndex);
    }

    public async System.Threading.Tasks.Task<ApiGetGenerator.Page<TemplateApiNet8.Database.Models.ShowTemp>> AutoGet(ShowQuery? query)
    {
        var set = DatabaseContext.Set<TemplateApiNet8.Database.Models.ShowTemp>().AsQueryable();

        var pageSize = query?.PageSize ?? 10;
        var pageIndex = query?.PageIndex ?? 0;

        set = set.Skip(pageSize * pageIndex);
        set = set.Take(pageSize);

        if (query is not null)
        {
            var includes = query.Includes;

            if (includes?.Count > 0 == true)
            {
                for (int includesIndex = 1; includesIndex < includes.Count; includesIndex++)
                {
                    var current = includes[includesIndex];

                    set = current switch
                    {
                        ShowTempIncludes.Schedules => set.Include(x => x.Schedules),

                    };
                }
            }

            var orderBy = query.OrderBy;
            var orderByCount = orderBy?.Count;

            if (orderByCount > 0 == true)
            {
                IOrderedQueryable<TemplateApiNet8.Database.Models.ShowTemp> orderedQueryable;

                var first = orderBy[0];

                if (first.Direction == OrderDirection.Descending)
                {
                    orderedQueryable = first.Column switch
                    {
                        ShowTempColumns.Id => set.OrderBy(x => x.Id),
                        ShowTempColumns.OnEmision => set.OrderBy(x => x.OnEmision),
                        ShowTempColumns.Name => set.OrderBy(x => x.Name),
                        ShowTempColumns.Runtime => set.OrderBy(x => x.Runtime),
                        ShowTempColumns.Premiered => set.OrderBy(x => x.Premiered),

                    };
                }
                else
                {
                    orderedQueryable = first.Column switch
                    {
                        ShowTempColumns.Id => set.OrderByDescending(x => x.Id),
                        ShowTempColumns.OnEmision => set.OrderByDescending(x => x.OnEmision),
                        ShowTempColumns.Name => set.OrderByDescending(x => x.Name),
                        ShowTempColumns.Runtime => set.OrderByDescending(x => x.Runtime),
                        ShowTempColumns.Premiered => set.OrderByDescending(x => x.Premiered),

                    };
                }

                for (int OrderByIndex = 1; OrderByIndex < orderByCount; OrderByIndex++)
                {
                    var current = orderBy[OrderByIndex];

                    if (current.Direction == OrderDirection.Descending)
                    {
                        orderedQueryable = current.Column switch
                        {
                            ShowTempColumns.Id => orderedQueryable.ThenBy(x => x.Id),
                            ShowTempColumns.OnEmision => orderedQueryable.ThenBy(x => x.OnEmision),
                            ShowTempColumns.Name => orderedQueryable.ThenBy(x => x.Name),
                            ShowTempColumns.Runtime => orderedQueryable.ThenBy(x => x.Runtime),
                            ShowTempColumns.Premiered => orderedQueryable.ThenBy(x => x.Premiered),

                        };
                    }
                    else
                    {
                        orderedQueryable = current.Column switch
                        {
                            ShowTempColumns.Id => orderedQueryable.ThenByDescending(x => x.Id),
                            ShowTempColumns.OnEmision => orderedQueryable.ThenByDescending(x => x.OnEmision),
                            ShowTempColumns.Name => orderedQueryable.ThenByDescending(x => x.Name),
                            ShowTempColumns.Runtime => orderedQueryable.ThenByDescending(x => x.Runtime),
                            ShowTempColumns.Premiered => orderedQueryable.ThenByDescending(x => x.Premiered),

                        };
                    }
                }

                set = orderedQueryable;
            }

            if (query.IdMax is not null)
            {
                set = set.Where(i => i.Id <= query.IdMax);
            }
            if (query.IdMin is not null)
            {
                set = set.Where(i => i.Id >= query.IdMin);
            }
            if (query.OnEmision is not null)
            {
                set = set.Where(i => i.OnEmision == query.OnEmision);
            }
            if (query.Name is not null)
            {
                set = query.NameComparationType switch
                {
                    StringComparationType.StartsWith => set.Where(i => i.Name.StartsWith(query.Name)),
                    StringComparationType.EndsWith => set.Where(i => i.Name.EndsWith(query.Name)),
                    StringComparationType.Contains => set.Where(i => i.Name.Contains(query.Name)),
                    StringComparationType.Equals => set.Where(i => i.Name == query.Name),
                    _ => set.Where(i => i.Name == query.Name),
                };
            }
            if (query.RuntimeMax is not null)
            {
                set = set.Where(i => i.Runtime <= query.RuntimeMax);
            }
            if (query.RuntimeMin is not null)
            {
                set = set.Where(i => i.Runtime >= query.RuntimeMin);
            }
            if (query.PremieredMax is not null)
            {
                set = set.Where(i => i.Premiered <= query.PremieredMax);
            }
            if (query.PremieredMin is not null)
            {
                set = set.Where(i => i.Premiered >= query.PremieredMin);
            }

        }

        var count = await set.CountAsync();

        return new(set, count, pageSize, pageIndex);
    }
}