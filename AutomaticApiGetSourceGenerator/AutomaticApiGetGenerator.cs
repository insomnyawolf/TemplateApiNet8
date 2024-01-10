using ApiGetGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGenerator;
using System.Text;
using System.Xml.Linq;

namespace AutomaticApiGetSourceGenerator;

// https://github.com/dotnet/roslyn-sdk/issues/850#issuecomment-1038725567
[Generator(LanguageNames.CSharp)]
public partial class AutomaticApiGetGenerator : IIncrementalGenerator
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

        // Generate the source
        context.RegisterSourceOutput(targetProvider, Execute);
    }

    public static void ThingsThatDoesNotDeppendOnUserCode(IncrementalGeneratorPostInitializationContext context)
    {
        // Add Unconditionally generated files 
        context.AddStaticFile("GenerateGetAttribute.cs");
    }

    public static bool IsTargetForGenerator(SyntaxNode SyntaxNode, CancellationToken cancellationToken)
    {
        if (SyntaxNode is not MethodDeclarationSyntax methodNode)
        {
            return false;
        }

        if (!methodNode.TryGetAttribute(nameof(GenerateGetAttribute), out var attributeSyntax))
        {
            return false;
        }

        return true;
    }

    public class HelperClass
    {
        public SyntaxNode SyntaxNode { get; set; }
        public IMethodSymbol IMethodSymbol { get; set; }

        public Diagnostic GetDiagnostic(DiagnosticDescriptor descriptor, string message)
        {
            var location = Location.Create(SyntaxNode.SyntaxTree, SyntaxNode.FullSpan);
            var diagnostic = Diagnostic.Create(descriptor, location, message);
            return diagnostic;
        }
    }

    public static HelperClass PrepareDataForGeneration(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        var temp = (IMethodSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node, cancellationToken)!;

        return new HelperClass()
        {
            SyntaxNode = context.Node,
            IMethodSymbol = temp,
        };
    }

    public static void Execute(SourceProductionContext context, HelperClass helperClass)
    {
        var method = helperClass.IMethodSymbol;

        var @return = method.ReturnType;

        if (@return is not INamedTypeSymbol rawReturn)
        {
            context.ReportDiagnostic(helperClass.GetDiagnostic(InvalidReturnType, "Unnamed Types Are Not Valid in this context"));
            return;
        }

        var returnTypeString = rawReturn.GetFullyQualifiedGenericsString();

        if (rawReturn.IsIAsyncResult())
        {
            rawReturn = (INamedTypeSymbol)rawReturn.TypeArguments[0];
        }

        if (!rawReturn.IsEnumerable())
        {
            context.ReportDiagnostic(helperClass.GetDiagnostic(InvalidReturnType, "The provided type is not an enumerable"));
            return;
        }

        if (rawReturn.TypeArguments.Length != 1)
        {
            context.ReportDiagnostic(helperClass.GetDiagnostic(InvalidReturnType, "The enumerable must have a single generic argument"));
            return;
        }

        var @params = method.Parameters;

        if (@params.Length < 1)
        {
            context.ReportDiagnostic(helperClass.GetDiagnostic(InvalidParams, "You didn't provide any params"));
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
        var queryParamType = (INamedTypeSymbol)queryParam.Type;

        string queryParamTypeName = queryParamType.GetUnderlyingNullableName();

        var queryParamName = queryParam.Name;

        // This type is the one that we will use for the query model and for the query.
        var dbType = rawReturn.TypeArguments[0];

        var queryModelSb = new StringBuilder();

        var getMethodSb = new StringBuilder();

        var members = dbType.GetMembers();

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

            var propertyType = (INamedTypeSymbol)property.GetMethod.ReturnType;

            if (propertyType.IsEnumerable())
            {
                continue;
            }

            string typeName = propertyType.GetUnderlyingNullableName();

            if (propertyType.IsString())
            {
                queryModelSb.Indent(1).AppendLine($"public {typeName}? {property.Name} {{ get; set; }}");

                getMethodSb.Indent(2).AppendLine($"if ({queryParamName}.{property.Name} is not null)");
                getMethodSb.Indent(2).AppendLine("{");
                getMethodSb.Indent(3).AppendLine($"set = set.Where(i => i.{property.Name}.Contains({queryParamName}.{property.Name}));");
                getMethodSb.Indent(2).AppendLine("}");
            }
            else if (propertyType.IsIComparable())
            {
                var name = $"{property.Name}Max";
                queryModelSb.Indent(1).AppendLine($"public {typeName}? {name} {{ get; set; }}");

                getMethodSb.Indent(2).AppendLine($"if ({queryParamName}.{name} is not null)");
                getMethodSb.Indent(2).AppendLine("{");
                getMethodSb.Indent(3).AppendLine($"set = set.Where(i => i.{property.Name} <= {queryParamName}.{name});");
                getMethodSb.Indent(2).AppendLine("}");


                name = $"{property.Name}Min";
                queryModelSb.Indent(1).AppendLine($"public {typeName}? {name} {{ get; set; }}");

                getMethodSb.Indent(2).AppendLine($"if ({queryParamName}.{name} is not null)");
                getMethodSb.Indent(2).AppendLine("{");
                getMethodSb.Indent(3).AppendLine($"set = set.Where(i => i.{property.Name} >= {queryParamName}.{name});");
                getMethodSb.Indent(2).AppendLine("}");
            }
            else
            {
                queryModelSb.Indent(1).AppendLine($"public {typeName}? {property.Name} {{ get; set; }}");

                getMethodSb.Indent(2).AppendLine($"if ({queryParamName}.{property.Name} is not null)");
                getMethodSb.Indent(2).AppendLine("{");
                getMethodSb.Indent(3).AppendLine($"set = set.Where(i => i.{property.Name} == {queryParamName}.{property.Name});");
                getMethodSb.Indent(2).AppendLine("}");
            }
        }

        //var content = sb.ToString();

        var replacements = new Dictionary<string, string>
        {
            { "Namespace", method.ContainingNamespace.ToString() },
            { "QueryModelClassName", queryParamTypeName },
            { "QueryModelContent", queryModelSb.ToString() },
            { "ControllerName", method.ContainingType.Name },
            { "DatabaseClassNameReturnEnumerable", returnTypeString },
            { "GetEndpointMethodName", method.Name },
            { "Params", paramsSb.ToString() },
            { "DatabaseClassName", dbType.GetFullyQualifiedName() },
            { "GetEndpointContent", getMethodSb.ToString() },
        };

        context.AddTemplate("BasePartialControllerTemplate.cs", method.GetFullyQualifiedName(), replacements);


        //if (rawReturn.TypeArguments.Length != 1)
        //{
        //    context.ReportDiagnostic(helperClass.GetDiagnostic(InvalidReturnType, "The enumerable must have a single generic argument"));
        //    return;
        //}

        //rawReturn

        //if ()
        //{
        //    context.ReportDiagnostic(Diagnostic.Create(InvalidReturnType, Location.None));
        //    return;
        //}

        //if ()
        //var @namespace = method.ContainingNamespace.ToString();
        //var @class = method.Name;
        //var generatedNameBase = $"{@namespace}.{@class}.{MethodName}";

        //var members = method.GetMembers();

        //var sb = new StringBuilder();

        //sb.AppendLine();
        //sb.Indent(2).AppendLine($"if ({ParameterName} is null)");
        //sb.Indent(3).AppendLine($"{ParameterName} = new {ParameterType}();");
        //sb.Indent(2).AppendLine($"else");
        //sb.Indent(3).AppendLine($"{ParameterName} = new {ParameterType}({ParameterName});");

        //sb.Indent(2).AppendLine($"{ParameterName}.Add(this.Id);");

        //foreach (var member in members)
        //{
        //    if (member is not IPropertySymbol property)
        //    {
        //        continue;
        //    }

        //    if (property.SetMethod is null)
        //    {
        //        continue;
        //    }

        //    if (property.GetMethod is null)
        //    {
        //        continue;
        //    }

        //    var type = property.GetMethod.ReturnType;

        //    if (available.Contains(type))
        //    {
        //        sb.Indent(2).AppendLine($"if ({property.Name} is not null)");
        //        sb.Indent(3).AppendLine($"if ({ParameterName}.Add({property.Name}.Id)){{{property.Name}.{MethodName}({ParameterName});}}else{{{property.Name} = null;}}");
        //    }
        //    else if (type is INamedTypeSymbol named && type.IsEnumerable() && type.GetFullyQualifiedName() != "System.String")
        //    {
        //        var args = named.TypeArguments;
        //        if (args.Length != 1)
        //        {
        //            continue;
        //        }

        //        if (!available.Contains(args[0]))
        //        {
        //            continue;
        //        }
        //        sb.Indent(2).AppendLine($"if ({property.Name} is not null)");
        //        sb.Indent(3).AppendLine($"for (int i = {property.Name}.Count - 1; i > 0; i--)");
        //        sb.Indent(3).AppendLine($"{{");
        //        sb.Indent(4).AppendLine($"var current = {property.Name}[i];");
        //        sb.Indent(4).AppendLine($"if(!existing.Add(current.Id)){{{property.Name}.RemoveAt(i);}}");
        //        sb.Indent(3).AppendLine($"}}");
        //        sb.Indent(3).AppendLine($"for (int i = {property.Name}.Count - 1; i > 0; i--)");
        //        sb.Indent(3).AppendLine($"{{");
        //        sb.Indent(4).AppendLine($"var current = {property.Name}[i];");
        //        sb.Indent(4).AppendLine($"current.CleanEntityFrameworkReferenceLoops(existing);");
        //        sb.Indent(3).AppendLine($"}}");
        //        //;
        //    }
        //}

        //var content = sb.ToString();

        //var replacements = new Dictionary<string, string>
        //{
        //    { "Namespace", @namespace },
        //    { "Class", method.Name },
        //    { "Content", content },
        //    { "MethodName", MethodName },
        //    { "ParameterType", ParameterType },
        //    { "ParameterName", ParameterName },
        //};

        //context.AddTemplate("PartialClassTemplate.cs", generatedNameBase, replacements);
    }

    //public static void BaseDefinition(SourceProductionContext context, INamedTypeSymbol current)
    //{
    //    var @namespace = current.ContainingNamespace.ToString();
    //    var @class = current.Name;
    //    var generatedNameBase = $"{@namespace}.{@class}.{MethodName}";

    //    var replacements = new Dictionary<string, string>
    //    {
    //        { "Namespace", @namespace },
    //        { "Class", current.Name },
    //        { "MethodName", MethodName },
    //        { "ParameterType", ParameterType },
    //        { "ParameterName", ParameterName },
    //    };

    //    context.AddTemplate("BasePartialClassTemplate.cs", generatedNameBase, replacements);
    //}
}