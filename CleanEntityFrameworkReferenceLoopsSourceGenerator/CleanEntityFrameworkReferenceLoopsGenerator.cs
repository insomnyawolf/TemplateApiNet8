using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGenerator;
using System.Collections.Immutable;
using System.Text;

namespace CleanEntityFrameworkReferenceLoopsSourceGenerator;

// https://github.com/dotnet/roslyn-sdk/issues/850#issuecomment-1038725567
[Generator(LanguageNames.CSharp)]
public class CleanEntityFrameworkReferenceLoopsGenerator : IIncrementalGenerator
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
        var temp = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node, cancellationToken)!;

        return temp;
    }

    private const string MethodName = "CleanEntityFrameworkReferenceLoops";
    private const string ParameterName = "existing";
    private const string ParameterType = "HashSet<Guid>";

    public static void SharedCode(SourceProductionContext context, INamedTypeSymbol envInfo)
    {
        // Add Unconditionally generated files 
        var @namespace = envInfo.ContainingNamespace.ToString();
        var classType = envInfo.BaseType!.GetFullyQualifiedName();

        context.AddTemplate("IEnumerableExtensions.cs", MethodName, new Dictionary<string, string>()
        {
            { "Namespace", @namespace },
            { "Class", MethodName+"Extensions" },
            { "MethodName", MethodName },
            { "BaseClass", classType },
            { "ParameterType", ParameterType },
            { "ParameterName", ParameterName },
        });
    }

    public static void ExecuteMany(SourceProductionContext context, ImmutableArray<INamedTypeSymbol> namedTypeSymbols)
    {
        INamedTypeSymbol? item = null;

        for (int i = 0; i < namedTypeSymbols.Length; i++)
        {
            item = namedTypeSymbols[i];
            Execute(context, item, namedTypeSymbols);
        }

        if (item is not null)
        {
            SharedCode(context, item);
        }
    }

    public static void Execute(SourceProductionContext context, INamedTypeSymbol current, ImmutableArray<INamedTypeSymbol> available)
    {
        var @namespace = current.ContainingNamespace.ToString();
        var @class = current.Name;
        var generatedNameBase = $"{@namespace}.{@class}.{MethodName}";

        var members = current.GetMembers();

        var sb = new StringBuilder();

        sb.AppendLine();
        sb.Indent(2).AppendLine($"if ({ParameterName} is null)");
        sb.Indent(3).AppendLine($"{ParameterName} = new {ParameterType}();");
        sb.Indent(2).AppendLine($"else");
        sb.Indent(3).AppendLine($"{ParameterName} = new {ParameterType}({ParameterName});");

        sb.Indent(2).AppendLine($"{ParameterName}.Add(this.Id);");

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
                sb.Indent(2).AppendLine($"if ({property.Name} is not null)");
                sb.Indent(3).AppendLine($"if ({ParameterName}.Add({property.Name}.Id)){{{property.Name}.{MethodName}({ParameterName});}}else{{{property.Name} = null;}}");
            }
            else if (type is INamedTypeSymbol named && type.IsEnumerable() && type.GetFullyQualifiedName() != "System.String")
            {
                var args = named.TypeArguments;
                if (args.Length != 1)
                {
                    continue;
                }

                if (!available.Contains(args[0]))
                {
                    continue;
                }
                sb.Indent(2).AppendLine($"if ({property.Name} is not null)");
                sb.Indent(3).AppendLine($"for (int i = {property.Name}.Count - 1; i > 0; i--)");
                sb.Indent(3).AppendLine($"{{");
                sb.Indent(4).AppendLine($"var current = {property.Name}[i];");
                sb.Indent(4).AppendLine($"if(!existing.Add(current.Id)){{{property.Name}.RemoveAt(i);}}");
                sb.Indent(3).AppendLine($"}}");
                sb.Indent(3).AppendLine($"for (int i = {property.Name}.Count - 1; i > 0; i--)");
                sb.Indent(3).AppendLine($"{{");
                sb.Indent(4).AppendLine($"var current = {property.Name}[i];");
                sb.Indent(4).AppendLine($"current.CleanEntityFrameworkReferenceLoops(existing);");
                sb.Indent(3).AppendLine($"}}");
                //;
            }
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