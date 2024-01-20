using ApiGetGenerator;
using AutomaticApiGetSourceGenerator.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorHelpers;

namespace AutomaticApiGetSourceGenerator;

//public partial class TestClass
//{
//    // weirdness that needs further investigation
//    // byte[]? type 
//    // TestClass? type
//
//    public byte[]? Datetime { get; set; }
//    public override Guid Id { get; set; }
//    public bool? OnEmision { get; set; }
//    public string? Name { get; set; }
//    public int? Runtime { get; set; }
//    public DateTimeOffset? Premiered { get; set; }
//    public IList<ScheduleDay>? Schedules { get; set; }
//}

// https://github.com/dotnet/roslyn-sdk/issues/850#issuecomment-1038725567
[Generator(LanguageNames.CSharp)]
public partial class GenerateFilterEndpointGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Add Unconditionally generated files 
        context.RegisterPostInitializationOutput(ThingsThatDoesNotDeppendOnUserCode);

        // Search For Targets And Prepare Them
        var targetProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: IsTargetForGenerator,
                transform: PrepareDataForGeneration);

        // Filter Invalid
        targetProvider = targetProvider.Where(i => i is not null);

        var targets = targetProvider.Collect();

        // Generate the source

        context.RegisterSourceOutput(targets, EnumsGenerator.Generate);
        context.RegisterSourceOutput(targets, QueryModelGenerator.Generate);
        context.RegisterSourceOutput(targets, EntityFrameworkGenerator.Generate);
        context.RegisterSourceOutput(targets, SolrGenerator.Generate);
    }

    public static void ThingsThatDoesNotDeppendOnUserCode(IncrementalGeneratorPostInitializationContext context)
    {
        // Add Unconditionally generated files 
        context.AddStaticFile("00SharedStatic.cs");
    }

    public static bool IsTargetForGenerator(SyntaxNode SyntaxNode, CancellationToken cancellationToken)
    {
        if (SyntaxNode is not MethodDeclarationSyntax methodNode)
        {
            return false;
        }

        return methodNode.AttributeLists.Any();
    }

    public static HelperClass PrepareDataForGeneration(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        var reference = context.Node.GetReference();

        var symbol = (IMethodSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node, cancellationToken)!;

        var attr = symbol.GetAttributes();

        AttributeData? targetAttribute = null;

        foreach (AttributeData attribute in attr)
        {
            if (attribute.AttributeClass!.InheritFrom(typeof(GenerateFilterAttribute).FullName))
            {
                targetAttribute = attribute;
                break;
            }
        }

        if (targetAttribute is null)
        {
            return null!;
        }

        return new HelperClass(reference, symbol, targetAttribute);
    }
}
