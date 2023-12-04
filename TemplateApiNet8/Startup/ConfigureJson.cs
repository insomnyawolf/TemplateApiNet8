using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using TemplateApiNet8.Startup.OData;
using System.Runtime.CompilerServices;
using TemplateApiNet8.Database.Models;

namespace TemplateApiNet8.Startup;

public static class Json
{
    public static void ConfigureJson(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureJsonOptions>();
    }
}

public class ConfigureJsonOptions : IConfigureNamedOptions<JsonOptions>
{
    public void Configure(string? name, JsonOptions options)
    {
        Configure(options);
    }

    public void Configure(JsonOptions options)
    {
        var so = options.JsonSerializerOptions;
        // Sane Defaults
        so.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        so.MaxDepth = 128;
    }
}

public sealed class CustomReferenceHandler : ReferenceHandler
{
    public CustomReferenceHandler() { }

    public override ReferenceResolver CreateResolver() => new CustomPreserveReferenceResolver();

    // Someday
    //internal override ReferenceResolver CreateResolver(bool writing) => new CustomPreserveReferenceResolver(writing);
}

internal sealed class CustomPreserveReferenceResolver : ReferenceResolver
{
    private uint _referenceCount;
    private readonly Dictionary<string, object>? ReferenceIdToObjectMap;
    private readonly Dictionary<object, string>? ObjectToReferenceIdMap;

    public CustomPreserveReferenceResolver(bool writing = true)
    {
        if (writing)
        {
            // Comparer used here does a reference equality comparison on serialization, which is where we use the objects as the dictionary keys.
            ObjectToReferenceIdMap = new Dictionary<object, string>(CustomReferenceEqualityComparer.Instance);
        }
        else
        {
            ReferenceIdToObjectMap = new Dictionary<string, object>();
        }
    }

    public override void AddReference(string referenceId, object value)
    {
        Debug.Assert(ReferenceIdToObjectMap != null);

        ReferenceIdToObjectMap.Add(referenceId, value);
    }

    public override string GetReference(object value, out bool alreadyExists)
    {
        Debug.Assert(ObjectToReferenceIdMap != null);

        if (ObjectToReferenceIdMap.TryGetValue(value, out string? referenceId))
        {
            alreadyExists = true;
        }
        else
        {
            _referenceCount++;
            referenceId = _referenceCount.ToString();
            ObjectToReferenceIdMap.Add(value, referenceId);
            alreadyExists = false;
        }

        return referenceId;
    }

    public override object ResolveReference(string referenceId)
    {
        Debug.Assert(ReferenceIdToObjectMap != null);

        return ReferenceIdToObjectMap[referenceId];
    }
}

sealed class CustomReferenceEqualityComparer : IEqualityComparer<object?>
{
    private CustomReferenceEqualityComparer() { }

    public static CustomReferenceEqualityComparer Instance { get; } = new CustomReferenceEqualityComparer();

    public new bool Equals(object? x, object? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is BaseEntity a && y is BaseEntity b)
        {
            return a.Id == b.Id;
        }

        return false;
    }

    public int GetHashCode(object? obj)
    {
        return RuntimeHelpers.GetHashCode(obj!);
    }
}