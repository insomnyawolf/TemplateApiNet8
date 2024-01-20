using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiGetGenerator;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public abstract class GenerateFilterAttribute : Attribute { }

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderDirection
{
    Ascending,
    Descending,
}

public class OrderBy<TEntityColumns> where TEntityColumns : notnull, Enum
{
    public OrderDirection Direction { get; set; }
    public TEntityColumns Column { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StringComparationType
{
    Equals,
    StartsWith,
    Contains,
    EndsWith,
}

public class Page<T>
{
    public long TotalResults { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Data { get; set; }

    public Page(IEnumerable<T> data, int count, int pageSize, int pageIndex)
    {
        var Quotient = Math.DivRem(count, pageSize, out var Remainder);

        if (Remainder == 0)
        {
            Quotient--;
        }

        TotalResults = count;
        PageSize = pageSize;
        PageIndex = pageIndex;
        TotalPages = Quotient;
        Data = data;
    }
}

public sealed class GenerateEntityFrameworkFilterAttribute : GenerateFilterAttribute
{
    // This is a named argument
    public string InyectedDatabaseContextName { get; set; }
}

public sealed class GenerateSolrFilterAttribute : GenerateFilterAttribute
{
    // This is a named argument
    public string InyectedSolrClientName { get; set; }
}

public static class GenerateSolrExtensions
{
    public static string GetValueOrAsterisk<TItem>(this TItem? item) where TItem : struct
    {
        if (item is null)
        {
            return "*";
        }

        return item.ToString();
    }
}
