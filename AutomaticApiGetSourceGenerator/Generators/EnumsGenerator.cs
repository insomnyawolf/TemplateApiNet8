using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text;
using SourceGeneratorHelpers;

namespace AutomaticApiGetSourceGenerator.Generators;

public class EnumsGenerator
{
    public static void Generate(SourceProductionContext context, ImmutableArray<HelperClass> items)
    {
        var distinct = items.Distinct(i => i.TargetTypeName);
        foreach (var item in distinct)
        {
            GenerateInternal(context, item);
        }
    }

    private static void GenerateInternal(SourceProductionContext context, HelperClass helperClass)
    {
        var columnsSb = new StringBuilder();

        var includesSb = new StringBuilder();

        foreach (var member in helperClass.ReturnTypeMembers)
        {
            if (member is not IPropertySymbol property)
            {
                continue;
            }

            if (property.SetMethod is null)
            {
                continue;
            }

            if (property.GetMethod is null)
            {
                continue;
            }

            var propertyType = property.GetMethod.ReturnType;

            if (!propertyType.IsString())
            {
                if (propertyType.IsEnumerable() || !propertyType.IsDefaultClass())
                {
                    includesSb.Indent(1).AppendLine($"{property.Name},");
                    continue;
                }
            }

            columnsSb.Indent(1).AppendLine($"{property.Name},");
        }

        var replacements = new Dictionary<string, string>
        {
            { "DatabaseClassColumnsName", helperClass.ColumnsEnumName },
            { "DatabaseClassColumns", columnsSb.ToString() },
            { "DatabaseClassIncludesName", helperClass.IncludesEnumName },
            { "DatabaseClassIncludes", includesSb.ToString() },
        };

        context.AddTemplate("Enums.cs", helperClass.TargetTypeName, replacements);
    }
}
