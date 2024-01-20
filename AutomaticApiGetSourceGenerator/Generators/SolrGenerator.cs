using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text;
using SourceGeneratorHelpers;
using ApiGetGenerator;

namespace AutomaticApiGetSourceGenerator.Generators;

public class SolrGenerator
{
    public static void Generate(SourceProductionContext context, ImmutableArray<HelperClass> items)
    {
        foreach (var item in items)
        {
            if (item.AttributeData.AttributeClass!.Name != nameof(GenerateSolrFilterAttribute))
            {
                continue;
            }
            GenerateInternal(context, item);
        }
    }

    public static void GenerateInternal(SourceProductionContext context, HelperClass helperClass)
    {
        object arg = helperClass.AttributeData.NamedArguments.GetByName(nameof(GenerateSolrFilterAttribute.InyectedSolrClientName));

        var dbContextVariableName = (string)arg;

        var filtersSb = new StringBuilder();

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
                    continue;
                }
            }

            if (propertyType.IsString())
            {                
                filtersSb.Indent(3).AppendLine($"if ({helperClass.QueryParamName}.{property.Name} is not null)");
                filtersSb.Indent(3).AppendLine("{");
                filtersSb.Indent(4).AppendLine($"var filterText = query.{helperClass.GetComparationTypeName(property.Name)} switch");
                filtersSb.Indent(4).AppendLine("{");
                filtersSb.Indent(5).AppendLine($"StringComparationType.StartsWith => $\"{property.Name}:{{{helperClass.QueryParamName}.{property.Name}}}*\",");
                filtersSb.Indent(5).AppendLine($"StringComparationType.EndsWith => $\"{property.Name}:*{{{helperClass.QueryParamName}.{property.Name}}}\",");
                filtersSb.Indent(5).AppendLine($"StringComparationType.Contains => $\"{property.Name}:*{{{helperClass.QueryParamName}.{property.Name}}}*\",");
                filtersSb.Indent(5).AppendLine($"StringComparationType.Equals => $\"{property.Name}:{{{helperClass.QueryParamName}.{property.Name}}}\",");
                filtersSb.Indent(5).AppendLine($"_ => $\"{property.Name}:{{{helperClass.QueryParamName}.{property.Name}}}\",");
                filtersSb.Indent(4).AppendLine("};");
                filtersSb.Indent(4).AppendLine("var filter = new SolrQuery(filterText);");
                filtersSb.Indent(4).AppendLine($"fqs.Add(filter);");
                filtersSb.Indent(3).AppendLine("}");
            }
            else if (propertyType.IsIComparable() && !propertyType.IsBoolean())
            {
                var maxName = helperClass.GetMaxName(property.Name);
                var minName = helperClass.GetMinName(property.Name);
                filtersSb.Indent(3).AppendLine($"if ({helperClass.QueryParamName}.{minName} is not null || {helperClass.QueryParamName}.{maxName} is not null)");
                filtersSb.Indent(3).AppendLine("{");
                filtersSb.Indent(4).AppendLine($"var min = {helperClass.QueryParamName}.{minName}.GetValueOrAsterisk();");
                filtersSb.Indent(4).AppendLine($"var max = {helperClass.QueryParamName}.{maxName}.GetValueOrAsterisk();");
                filtersSb.Indent(4).AppendLine($"var filter = new SolrQuery($\"{property.Name}:[{{min}} TO {{max}}]\");");
                filtersSb.Indent(4).AppendLine($"fqs.Add(filter);");
                filtersSb.Indent(3).AppendLine("}");
            }
            else
            {
                filtersSb.Indent(3).AppendLine($"if ({helperClass.QueryParamName}.{property.Name} is not null)");
                filtersSb.Indent(3).AppendLine("{");
                filtersSb.Indent(4).AppendLine($"var filter = new SolrQuery($\"{property.Name}:{{{helperClass.QueryParamName}.{property.Name}}}\");");
                filtersSb.Indent(4).AppendLine($"fqs.Add(filter);");
                filtersSb.Indent(3).AppendLine("}");
            }
        }

        var replacements = new Dictionary<string, string>
        {
            { "Namespace", helperClass.Namespace },
            { "ControllerName", helperClass.MethodSymbol.ContainingType.Name },
            { "SolrClientVariableName", dbContextVariableName },
            { "ReturnString", helperClass.ReturnTypeString },
            { "GetEndpointMethodName", helperClass.MethodSymbol.Name },
            { "Params", helperClass.GetParamsString() },
            { "DatabaseClassName", helperClass.TargetType.GetFullyQualifiedName() },
            { "QueryParamName", helperClass.QueryParamName },
            { "GetFilters", filtersSb.ToString() },
        };

        context.AddTemplate("Solr.cs", helperClass.MethodSymbol.GetFullyQualifiedName(), replacements);
    }
}
