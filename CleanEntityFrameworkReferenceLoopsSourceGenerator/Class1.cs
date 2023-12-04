using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using SourceGenerator;
using System;
using System.Collections.Immutable;
using System.Text;
using System.Reflection.Metadata;

namespace CleanEntityFrameworkReferenceLoopsSourceGenerator;

// https://github.com/dotnet/roslyn-sdk/issues/850#issuecomment-1038725567
[Generator(LanguageNames.CSharp)]
public class Class1 : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Do a simple filter
        var databaseModelsProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: IsTargetForGenerator,
                transform: PrepareDataForGeneration);

        var collected = databaseModelsProvider.Collect();

        // Generate the source
        context.RegisterSourceOutput(collected, ExecuteMany);

        // Do a simple filter
        var databaseBaseModel = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: IsTargetForBase,
                transform: PrepareDataForGeneration);

        // Generate the source
        context.RegisterSourceOutput(databaseBaseModel, BaseDefinition);
    }

    const string BaseClassName = "BaseEntity";

    public static bool IsTargetForBase(SyntaxNode SyntaxNode, CancellationToken cancellationToken)
    {
        if (SyntaxNode is not ClassDeclarationSyntax classNode)
        {
            return false;
        }

        var text = classNode.Identifier.ToString();
        if (text == BaseClassName)
        {
            return true;
        }

        return false;
    }

    public static bool IsTargetForGenerator(SyntaxNode SyntaxNode, CancellationToken cancellationToken)
    {
        // true if it's what we are looking for, in that case all clases in the specified namespace
        if (SyntaxNode is not ClassDeclarationSyntax classNode)
        {
            return false;
        }

        if (classNode.BaseList is null)
        {
            return false;
        }

        foreach (BaseTypeSyntax type in classNode.BaseList.Types)
        {
            var text = type.ToString();
            if (text == BaseClassName)
            {
                return true;
            }
        }

        return false;
    }

    public static INamedTypeSymbol PrepareDataForGeneration(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        var temp = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node);

        return temp;
    }

    private const string MethodName = "CleanEntityFrameworkReferenceLoops";
    private const string ParameterName = "existing";
    private const string ParameterType = "HashSet<Guid>";
    private const string Indent = "\t\t";

    public static void SharedCode(SourceProductionContext context, INamedTypeSymbol envInfo)
    {
        // Add Unconditionally generated files 
        var @namespace = envInfo.ContainingNamespace.ToString();
        var classType = envInfo.BaseType.GetFullyQualifiedName();

        context.AddTemplate("IEnumerableExtensions.cs", MethodName, new Dictionary<string, string>()
        {

            { "Namespace", @namespace },
            { "Class", MethodName+"Extensions" },
            { "MethodName", MethodName },
            { "ParameterType", classType },
            { "ParameterName", ParameterName },
        });
    }

    public static void ExecuteMany(SourceProductionContext context, ImmutableArray<INamedTypeSymbol> namedTypeSymbols)
    {
        INamedTypeSymbol item = null;

        for (int i = 0; i < namedTypeSymbols.Length; i++)
        {
            item = namedTypeSymbols[i];
            Execute(context, item, namedTypeSymbols);
        }

        SharedCode(context, item);
    }

    public static void Execute(SourceProductionContext context, INamedTypeSymbol current, ImmutableArray<INamedTypeSymbol> available)
    {
        var @namespace = current.ContainingNamespace.ToString();
        var @class = current.Name;
        var generatedNameBase = $"{@namespace}.{@class}.{MethodName}";

        var members = current.GetMembers();

        var sb = new StringBuilder();

        sb.AppendLine($"{ParameterName} ??= new {ParameterType}();");
        sb.Append(Indent).AppendLine($"{ParameterName}.Add(this.Id);");

        foreach (var member in members)
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

            var type = property.GetMethod.ReturnType;

            if (available.Contains(type))
            {
                sb.Append(Indent).AppendLine($"if ({ParameterName}.Add({property.Name}.Id)){{{property.Name}.{MethodName}({ParameterName});}}else{{{property.Name} = null;}}");
            }
            // Add Collection Support
            //else if ()
            //{

            //}
        }

        var content = sb.ToString();

        var replacements = new Dictionary<string, string>
        {
            { "Namespace", @namespace },
            { "Class", current.Name },
            { "Content", content },
            { "MethodName", MethodName },
            { "ParameterType", ParameterType },
            { "ParameterName", ParameterName },
        };

        context.AddTemplate("PartialClassTemplate.cs", generatedNameBase, replacements);
    }

    public static void BaseDefinition(SourceProductionContext context, INamedTypeSymbol current)
    {
        var @namespace = current.ContainingNamespace.ToString();
        var @class = current.Name;
        var generatedNameBase = $"{@namespace}.{@class}.{MethodName}";

        var replacements = new Dictionary<string, string>
        {
            { "Namespace", @namespace },
            { "Class", current.Name },
            { "MethodName", MethodName },
            { "ParameterType", ParameterType },
            { "ParameterName", ParameterName },
        };

        context.AddTemplate("BasePartialClassTemplate.cs", generatedNameBase, replacements);
    }
}