﻿using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text;
using SourceGeneratorHelpers;

namespace AutomaticApiGetSourceGenerator.Generators;

public class EntityFrameworkGenerator
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
        var filtersSb = new StringBuilder();
        
        var orderBySb = new StringBuilder();
        var orderByDescendingSb = new StringBuilder();
        
        var thenBySb = new StringBuilder();
        var thenByDescendingSb = new StringBuilder();

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

            var propertyType = (INamedTypeSymbol)property.GetMethod.ReturnType;

            if (!propertyType.IsString() && propertyType.IsEnumerable())
            {
                includesSb.Indent(6).AppendLine($"{helperClass.IncludesEnumName}.{property.Name} => set.Include(x => x.{property.Name}),");
                continue;
            }

            orderBySb.Indent(6).AppendLine($"{helperClass.ColumnsEnumName}.{property.Name} => set.OrderBy(x => x.{property.Name}),");
            orderByDescendingSb.Indent(6).AppendLine($"{helperClass.ColumnsEnumName}.{property.Name} => set.OrderByDescending(x => x.{property.Name}),");

            thenBySb.Indent(7).AppendLine($"{helperClass.ColumnsEnumName}.{property.Name} => orderedQueryable.ThenBy(x => x.{property.Name}),");
            thenByDescendingSb.Indent(7).AppendLine($"{helperClass.ColumnsEnumName}.{property.Name} => orderedQueryable.ThenByDescending(x => x.{property.Name}),");

            if (propertyType.IsString())
            {
                filtersSb.Indent(3).AppendLine($"if ({helperClass.QueryParamName}.{property.Name} is not null)");
                filtersSb.Indent(3).AppendLine("{");
                filtersSb.Indent(4).AppendLine($"set = query.{helperClass.GetComparationTypeName(property.Name)} switch");
                filtersSb.Indent(4).AppendLine("{");
                filtersSb.Indent(5).AppendLine($"StringComparationType.StartsWith => set.Where(i => i.{property.Name}.StartsWith({helperClass.QueryParamName}.{property.Name})),");
                filtersSb.Indent(5).AppendLine($"StringComparationType.EndsWith => set.Where(i => i.{property.Name}.EndsWith({helperClass.QueryParamName}.{property.Name})),");
                filtersSb.Indent(5).AppendLine($"StringComparationType.Contains => set.Where(i => i.{property.Name}.Contains({helperClass.QueryParamName}.{property.Name})),");
                filtersSb.Indent(5).AppendLine($"StringComparationType.Equals => set.Where(i => i.{property.Name} == {helperClass.QueryParamName}.{property.Name}),");
                filtersSb.Indent(5).AppendLine($"_ => set.Where(i => i.{property.Name} == {helperClass.QueryParamName}.{property.Name}),");
                filtersSb.Indent(4).AppendLine("};");
                filtersSb.Indent(3).AppendLine("}");
            }
            else if (propertyType.IsIComparable() && !propertyType.IsBoolean())
            {
                var maxName = helperClass.GetMaxName(property.Name);
                filtersSb.Indent(3).AppendLine($"if ({helperClass.QueryParamName}.{maxName} is not null)");
                filtersSb.Indent(3).AppendLine("{");
                filtersSb.Indent(4).AppendLine($"set = set.Where(i => i.{property.Name} <= {helperClass.QueryParamName}.{maxName});");
                filtersSb.Indent(3).AppendLine("}");

                var minName = helperClass.GetMinName(property.Name);
                filtersSb.Indent(3).AppendLine($"if ({helperClass.QueryParamName}.{minName} is not null)");
                filtersSb.Indent(3).AppendLine("{");
                filtersSb.Indent(4).AppendLine($"set = set.Where(i => i.{property.Name} >= {helperClass.QueryParamName}.{minName});");
                filtersSb.Indent(3).AppendLine("}");
            }
            else
            {
                filtersSb.Indent(3).AppendLine($"if ({helperClass.QueryParamName}.{property.Name} is not null)");
                filtersSb.Indent(3).AppendLine("{");
                filtersSb.Indent(4).AppendLine($"set = set.Where(i => i.{property.Name} == {helperClass.QueryParamName}.{property.Name});");
                filtersSb.Indent(3).AppendLine("}");
            }
        }

        var replacements = new Dictionary<string, string>
        {
            { "Namespace", helperClass.Namespace },
            { "ControllerName", helperClass.MethodSymbol.ContainingType.Name },
            { "ReturnString", helperClass.ReturnTypeString },
            { "GetEndpointMethodName", helperClass.MethodSymbol.Name },
            { "Params", helperClass.GetParamsString() },
            { "DatabaseClassName", helperClass.TargetTypeName },
            { "QueryParamName", helperClass.QueryParamName },
            { "Include", includesSb.ToString() },
            { "OrderBy", orderBySb.ToString() },
            { "OrderByDescending", orderByDescendingSb.ToString() },
            { "ThenOrderBy", thenBySb.ToString() },
            { "ThenOrderByDescending", thenByDescendingSb.ToString() },
            { "GetFilters", filtersSb.ToString() },
        };

        context.AddTemplate("EntityFramework.cs", helperClass.MethodSymbol.GetFullyQualifiedName(), replacements);
    }
}
