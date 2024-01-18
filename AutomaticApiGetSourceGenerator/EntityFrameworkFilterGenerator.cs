//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using SourceGeneratorHelpers;
//using System.Text;

//// To do
//// Extensibilidad mediante clase parcial

//namespace AutomaticApiGetSourceGenerator;

//// https://github.com/dotnet/roslyn-sdk/issues/850#issuecomment-1038725567
//[Generator(LanguageNames.CSharp)]
//public partial class EntityFrameworkFilterGenerator : IIncrementalGenerator
//{
//    public void Initialize(IncrementalGeneratorInitializationContext context)
//    {
//        // Search For Targets And Prepare Them
//        var targetProvider = context.SyntaxProvider
//            .CreateSyntaxProvider(
//                predicate: IsTargetForGenerator,
//                transform: PrepareDataForGeneration);

//        // Generate the source
//        context.RegisterSourceOutput(targetProvider, Execute);
//    }

//    public static bool IsTargetForGenerator(SyntaxNode SyntaxNode, CancellationToken cancellationToken)
//    {
//        if (SyntaxNode is not MethodDeclarationSyntax methodNode)
//        {
//            return false;
//        }

//        if (!methodNode.TryGetAttribute("GenerateGetAttribute", out var attributeSyntax))
//        {
//            return false;
//        }

//        return true;
//    }

//    public class HelperClass
//    {
//        public SyntaxNode SyntaxNode { get; set; }
//        public IMethodSymbol IMethodSymbol { get; set; }

//        public Diagnostic GetDiagnostic(DiagnosticDescriptor descriptor, string message)
//        {
//            var location = Location.Create(SyntaxNode.SyntaxTree, SyntaxNode.FullSpan);
//            var diagnostic = Diagnostic.Create(descriptor, location, message);
//            return diagnostic;
//        }
//    }

//    public static HelperClass PrepareDataForGeneration(GeneratorSyntaxContext context, CancellationToken cancellationToken)
//    {
//        var temp = (IMethodSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node, cancellationToken)!;

//        return new HelperClass()
//        {
//            SyntaxNode = context.Node,
//            IMethodSymbol = temp,
//        };
//    }

//    private static Dictionary<string, HashSet<string>> ExistingItemsPerNamespace = new Dictionary<string, HashSet<string>>();

//    public static void Execute(SourceProductionContext context, HelperClass helperClass)
//    {


        

//        var queryModelSb = new StringBuilder();

//        var getMethodSb = new StringBuilder();

//        var columnsSb = new StringBuilder();
//        var columnsEnumName = dbType.Name + "Columns";

//        var orderBySb = new StringBuilder();
//        var orderByDescendingSb = new StringBuilder();
//        var thenBySb = new StringBuilder();
//        var thenByDescendingSb = new StringBuilder();

//        var includesSb = new StringBuilder();
//        var includesEnumName = dbType.Name + "Includes";

//        var includeContent = new StringBuilder();

//        var members = dbType.GetMembers();


//        columnsSb.Indent(0).AppendLine("");
//        columnsSb.Indent(0).AppendLine($"");
//        columnsSb.Indent(0).AppendLine("{");


//        includesSb.Indent(0).AppendLine("[JsonConverter(typeof(JsonStringEnumConverter))]");
//        includesSb.Indent(0).AppendLine($"public enum {includesEnumName}");
//        includesSb.Indent(0).AppendLine("{");


//        foreach (var member in members)
//        {
//            if (member is not IPropertySymbol property)
//            {
//                continue;
//            }

//            if (property.SetMethod is null)
//            {
//                continue;
//            }

//            if (property.GetMethod is null)
//            {
//                continue;
//            }

//            var propertyType = (INamedTypeSymbol)property.GetMethod.ReturnType;

//            if (!propertyType.IsString() && propertyType.IsEnumerable())
//            {
//                includesSb.Indent(1).AppendLine($"{property.Name},");
//                includeContent.Indent(6).AppendLine($"{includesEnumName}.{property.Name} => set.Include(x => x.{property.Name}),");
//                continue;
//            }

//            columnsSb.Indent(1).AppendLine($"{property.Name},");

//            orderBySb.Indent(6).AppendLine($"{columnsEnumName}.{property.Name} => set.OrderBy(x => x.{property.Name}),");
//            orderByDescendingSb.Indent(6).AppendLine($"{columnsEnumName}.{property.Name} => set.OrderByDescending(x => x.{property.Name}),");

//            thenBySb.Indent(7).AppendLine($"{columnsEnumName}.{property.Name} => orderedQueryable.ThenBy(x => x.{property.Name}),");
//            thenByDescendingSb.Indent(7).AppendLine($"{columnsEnumName}.{property.Name} => orderedQueryable.ThenByDescending(x => x.{property.Name}),");

//            string typeName = propertyType.GetUnderlyingNullableName();

//            if (propertyType.IsString())
//            {
//                queryModelSb.Indent(1).AppendLine($"public {typeName}? {property.Name} {{ get; set; }}");

//                var comparationTypePeopName = $"{property.Name}ComparationType";
//                queryModelSb.Indent(1).AppendLine($"public StringComparationType? {comparationTypePeopName} {{ get; set; }}");

//                getMethodSb.Indent(3).AppendLine($"if ({queryParamName}.{property.Name} is not null)");
//                getMethodSb.Indent(3).AppendLine("{");
//                getMethodSb.Indent(4).AppendLine($"set = query.{comparationTypePeopName} switch");
//                getMethodSb.Indent(4).AppendLine("{");
//                getMethodSb.Indent(5).AppendLine($"StringComparationType.StartsWith => set.Where(i => i.Name.StartsWith(query.Name)),");
//                getMethodSb.Indent(5).AppendLine($"StringComparationType.EndsWith => set.Where(i => i.Name.EndsWith(query.Name)),");
//                getMethodSb.Indent(5).AppendLine($"StringComparationType.Contains => set.Where(i => i.Name.Contains(query.Name)),");
//                getMethodSb.Indent(5).AppendLine($"StringComparationType.Equals => set.Where(i => i.Name == query.Name),");
//                getMethodSb.Indent(5).AppendLine($"_ => set.Where(i => i.Name == query.Name),");
//                getMethodSb.Indent(4).AppendLine("};");
//                getMethodSb.Indent(3).AppendLine("}");
//            }
//            else if (propertyType.IsIComparable() && !propertyType.IsBoolean())
//            {
//                var name = $"{property.Name}Max";
//                queryModelSb.Indent(1).AppendLine($"public {typeName}? {name} {{ get; set; }}");

//                getMethodSb.Indent(3).AppendLine($"if ({queryParamName}.{name} is not null)");
//                getMethodSb.Indent(3).AppendLine("{");
//                getMethodSb.Indent(4).AppendLine($"set = set.Where(i => i.{property.Name} <= {queryParamName}.{name});");
//                getMethodSb.Indent(3).AppendLine("}");


//                name = $"{property.Name}Min";
//                queryModelSb.Indent(1).AppendLine($"public {typeName}? {name} {{ get; set; }}");

//                getMethodSb.Indent(3).AppendLine($"if ({queryParamName}.{name} is not null)");
//                getMethodSb.Indent(3).AppendLine("{");
//                getMethodSb.Indent(4).AppendLine($"set = set.Where(i => i.{property.Name} >= {queryParamName}.{name});");
//                getMethodSb.Indent(3).AppendLine("}");
//            }
//            else
//            {
//                queryModelSb.Indent(1).AppendLine($"public {typeName}? {property.Name} {{ get; set; }}");

//                getMethodSb.Indent(3).AppendLine($"if ({queryParamName}.{property.Name} is not null)");
//                getMethodSb.Indent(3).AppendLine("{");
//                getMethodSb.Indent(4).AppendLine($"set = set.Where(i => i.{property.Name} == {queryParamName}.{property.Name});");
//                getMethodSb.Indent(3).AppendLine("}");
//            }
//        }

//        columnsSb.Indent(0).AppendLine("}");
//        includesSb.Indent(0).AppendLine("}");

//        if (!existingItems.Add(columnsEnumName))
//        {
//            columnsSb.Clear();
//        }

//        if (!existingItems.Add(includesEnumName))
//        {
//            includesSb.Clear();
//        }

//        var replacements = new Dictionary<string, string>
//        {
//            { "Namespace", @namespace },
//            { "QueryModelClassName", queryParamTypeName },
//            { "QueryModelContent", queryModelSb.ToString() },
//            { "ControllerName", method.ContainingType.Name },
//            { "DatabaseClassNameReturnEnumerable", returnTypeString },
//            { "GetEndpointMethodName", method.Name },
//            { "Params", paramsSb.ToString() },
//            { "DatabaseClassName", dbType.GetFullyQualifiedName() },
//            { "GetEndpointContent", getMethodSb.ToString() },
//            { "QueryParamName", queryParamName },
//            { "DatabaseClassColumnsName", columnsEnumName },
//            { "DatabaseClassColumns", columnsSb.ToString() },
//            { "DatabaseClassIncludesName", includesEnumName },
//            { "DatabaseClassIncludes", includesSb.ToString() },
//            { "OrderBy", orderBySb.ToString() },
//            { "OrderByDescending", orderByDescendingSb.ToString() },
//            { "ThenOrderBy", thenBySb.ToString() },
//            { "ThenOrderByDescending", thenByDescendingSb.ToString() },
//            { "Include", includeContent.ToString() },
//        };

//        context.AddTemplate("BasePartialControllerTemplate.cs", method.GetFullyQualifiedName(), replacements);
//    }
//}