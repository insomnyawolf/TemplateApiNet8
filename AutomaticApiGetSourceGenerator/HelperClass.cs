using Microsoft.CodeAnalysis;
using SourceGeneratorHelpers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace AutomaticApiGetSourceGenerator;

public class HelperClass
{
    private static readonly DiagnosticDescriptor InvalidReturnType = new(
    id: "InvalidReturnType",
    title: "InvalidReturnType",
    messageFormat: "Return type must be a generic IEnumerable of a single type.{0}",
    category: "Funtionality",
    defaultSeverity: DiagnosticSeverity.Error,
    isEnabledByDefault: true,
    description: "Method must return a generic enumerable of an object");

    private static readonly DiagnosticDescriptor InvalidParams = new(
        id: "InvalidParams",
        title: "InvalidParams",
        messageFormat: "Method must have at least 1 param and the first param should be the query model which will be automatically created as partial class.{0}",
        category: "Funtionality",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Method must have at least 1 param and the first param should be the query model which will be automatically created as partial class.{0}");

    public SyntaxReference SyntaxReference { get; }
    public IMethodSymbol ISymbol { get; }
    public AttributeData AttributeData { get; }
    public List<Diagnostic> Diagnostics { get; } = new();
    public bool SkipGenerator { get; }

    public string Namespace { get; }


    // This type is the one that we will use for the query model and for the query.
    public string ReturnTypeString { get; }
    public ITypeSymbol TargetType { get; }
    public string TargetTypeName { get; }
    public ImmutableArray<ISymbol> ReturnTypeMembers { get; }


    public string QueryParamName { get; }
    public INamedTypeSymbol QueryParamType { get; }
    public string QueryParamTypeName { get; }


    public string ColumnsEnumName { get; }
    public string IncludesEnumName { get; }



    public HelperClass(SyntaxReference SyntaxReference, IMethodSymbol ISymbol, AttributeData AttributeData)
    {
        this.ISymbol = ISymbol;
        this.AttributeData = AttributeData;
        this.SyntaxReference = SyntaxReference;

        var method = ISymbol;

        Namespace = method.ContainingNamespace.ToString();

        var @return = method.ReturnType;

        if (@return is not INamedTypeSymbol rawReturn)
        {
            var diagnostic = GetDiagnostic(InvalidReturnType, "Unnamed Types Are Not Valid in this context");
            Diagnostics.Add(diagnostic);
            SkipGenerator = true;
            return;
        }

        ReturnTypeString = rawReturn.GetFullyQualifiedGenericsString();

        if (rawReturn.IsIAsyncResult())
        {
            rawReturn = (INamedTypeSymbol)rawReturn.TypeArguments[0];
        }

        if (!rawReturn.GetFullyQualifiedName().StartsWith("ApiGetGenerator.Page"))
        {
            var diagnostic = GetDiagnostic(InvalidReturnType, "The type returned must be a Page<T>");
            Diagnostics.Add(diagnostic);
            SkipGenerator = true;
            return;
        }

        if (rawReturn.TypeArguments.Length != 1)
        {
            var diagnostic = GetDiagnostic(InvalidReturnType, "The return type must have a single generic argument and it must be the database type");
            Diagnostics.Add(diagnostic);
            SkipGenerator = true;
            return;
        }

        TargetType = rawReturn.TypeArguments[0];

        TargetTypeName = TargetType.Name;

        var @params = method.Parameters;

        if (@params.Length < 1)
        {
            var diagnostic = GetDiagnostic(InvalidParams, "You didn't provide any params");
            Diagnostics.Add(diagnostic);
            SkipGenerator = true;
            return;
        }

        var paramsSb = new StringBuilder();

        foreach (var item in @params)
        {
            if (paramsSb.Length > 0)
            {
                paramsSb.Append(", ");
            }

            paramsSb.Append(item.ToString());
        }

        var queryParam = @params.FirstOrDefault();

        QueryParamName = queryParam.Name;

        QueryParamType = (INamedTypeSymbol)queryParam.Type;

        QueryParamTypeName = QueryParamType.GetUnderlyingNullableName();


        ColumnsEnumName = TargetTypeName + "Columns";
        IncludesEnumName = TargetTypeName + "Includes";

        ReturnTypeMembers = TargetType.GetMembers();
    }

    public bool IsValidState(SourceProductionContext SourceProductionContext)
    {
        foreach (var item in Diagnostics)
        {
            SourceProductionContext.ReportDiagnostic(item);
        }

        return SkipGenerator;
    }

    public string GetComparationTypeName(string Name)
    {
        return $"{Name}Comparer";
    }

    public string GetMaxName(string Name)
    {
        return $"{Name}Max";
    }

    public string GetMinName(string Name)
    {
        return $"{Name}Min";
    }

    private Diagnostic GetDiagnostic(DiagnosticDescriptor descriptor, string message)
    {
        var location = Location.Create(SyntaxReference.SyntaxTree, SyntaxReference.Span);
        var diagnostic = Diagnostic.Create(descriptor, location, message);
        return diagnostic;
    }
}