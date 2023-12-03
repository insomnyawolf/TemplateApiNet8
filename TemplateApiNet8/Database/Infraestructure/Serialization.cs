using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace TemplateApiNet8.Database.Infraestructure;

// Experimental, does what it should but probably it's not the best solution
public class DatabaseModelsSerialization
{
    private static readonly Type ExcludedAttributeCache = typeof(ForeignKeyAttribute);
    private static readonly Type ForceIncludeAttributeCache = typeof(JsonIncludeAttribute);

    public static void RemoveForeignKeyProperties(JsonTypeInfo jsonTypeInfo)
    {
        if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
            return;

        var props = jsonTypeInfo.Properties;

        for (int i = props.Count -1; i > -1; i--)
        {
            JsonPropertyInfo? current = props[i];

            var attrProv = current.AttributeProvider;

            if (attrProv is null)
            {
                continue;
            }

            if (attrProv.IsDefined(ForceIncludeAttributeCache, false))
            {
                continue;
            }

            if (!attrProv.IsDefined(ExcludedAttributeCache, false))
            {
                continue;
            }

            props.RemoveAt(i);
        }
    }
}
