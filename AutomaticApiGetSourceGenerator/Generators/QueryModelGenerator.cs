using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text;
using SourceGeneratorHelpers;

namespace AutomaticApiGetSourceGenerator.Generators;

public class QueryModelGenerator
{
    public static void Generate(SourceProductionContext context, ImmutableArray<HelperClass> items)
    {
        var distinct = items.Distinct(i => i.QueryParamTypeName);
        foreach (var item in distinct)
        {
            GenerateInternal(context, item);
        }
    }

    public static void GenerateInternal(SourceProductionContext context, HelperClass helperClass)
    {
        var queryModelSb = new StringBuilder();

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

            var propertyType = (INamedTypeSymbol)property.GetMethod.ReturnType;

            if (!propertyType.IsString() && propertyType.IsEnumerable())
            {
                continue;
            }

            string typeName = propertyType.GetUnderlyingNullableName();

            if (propertyType.IsString())
            {
                queryModelSb.Indent(1).AppendLine($"public {typeName}? {property.Name} {{ get; set; }}");

                var comparationTypePeopName = helperClass.GetComparationTypeName(property.Name);

                queryModelSb.Indent(1).AppendLine($"public StringComparationType? {comparationTypePeopName} {{ get; set; }}");
            }
            else if (propertyType.IsIComparable() && !propertyType.IsBoolean())
            {
                var maxName = helperClass.GetMaxName(property.Name);
                queryModelSb.Indent(1).AppendLine($"public {typeName}? {maxName} {{ get; set; }}");

                var minName = helperClass.GetMinName(property.Name);
                queryModelSb.Indent(1).AppendLine($"public {typeName}? {minName} {{ get; set; }}");
            }
            else
            {
                queryModelSb.Indent(1).AppendLine($"public {typeName}? {property.Name} {{ get; set; }}");
            }
        }

        var replacements = new Dictionary<string, string>
        {
            { "Namespace", helperClass.Namespace },
            { "QueryModelClassName", helperClass.QueryParamTypeName },
            { "DatabaseClassColumnsName", helperClass.ColumnsEnumName },
            { "DatabaseClassIncludesName", helperClass.IncludesEnumName },
            { "QueryModelContent", queryModelSb.ToString() },
        };

        context.AddTemplate("QueryModel.cs", helperClass.QueryParamTypeName, replacements);
    }
}
