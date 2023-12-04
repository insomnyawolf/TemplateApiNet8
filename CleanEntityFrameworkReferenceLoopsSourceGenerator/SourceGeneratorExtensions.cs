using CleanEntityFrameworkReferenceLoopsSourceGenerator;
using Microsoft.CodeAnalysis;
using System.Text.RegularExpressions;

namespace SourceGenerator
{
    public static class SourceGeneratorExtensions
    {
        private static readonly Type AssemblyType = typeof(Class1);

        public static Stream GetEmbedFile(string filename)
        {
            const string folderName = "EmbedResources";
#warning this may fail if reusing this extensions without updating the type in AssemblyType
            var path = $"{AssemblyType.Namespace}.{folderName}.{filename}";
            var asm = AssemblyType.Assembly;
            var resource = asm.GetManifestResourceStream(path);
            return resource;
        }

        private static readonly Dictionary<string, string> FileCache = new Dictionary<string, string>();

        public static string GetEmbedFileAsString(string filename)
        {
            if (FileCache.TryGetValue(filename, out var value))
            {
                return value;
            }

            using var embedFile = GetEmbedFile(filename);
            using var reader = new StreamReader(embedFile);
            string text = reader.ReadToEnd();

            FileCache.Add(filename, text);
            return text;
        }

        public static string FillTemplateFromFile(string filename, Dictionary<string, string> replacements)
        {
            var template = GetEmbedFileAsString(filename);

            return FillTemplate(template, replacements);
        }

        public static string FillTemplate(string template, Dictionary<string, string> replacements)
        {
            return Regex.Replace(template, @"Template(.+?)Template", m =>
            {
                if (!replacements.TryGetValue(m.Groups[1].Value, out var value))
                {
                    return "ERROR PATTERN NOT FOUND";
                }

                return value;
            });
        }

        public static bool HasToken(this SyntaxTokenList tokenList, string tokenValue)
        {
            for (int i = 0; i < tokenList.Count; i++)
            {
                var item = tokenList[i];

                if (item.Text == tokenValue)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool InheritFrom(this INamedTypeSymbol symbol, string fullyQualifiedName)
        {
            while (symbol.BaseType is not null)
            {
                symbol = symbol.BaseType;

                var name = symbol.ToString();

                if (name == fullyQualifiedName)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsDictionary(this ITypeSymbol symbol)
        {
            return symbol.HasInterface(typeof(IDictionary<,>).Name);
        }

        public static string GetFullyQualifiedGenericsString(this INamedTypeSymbol symbol)
        {
            var @base = symbol.GetFullyQualifiedName();

            var typeArguments = symbol.TypeArguments;

            var genericStringComponents = new List<string>();

            foreach (var type in typeArguments)
            {
                if (type is INamedTypeSymbol subSymbol)
                {
                    var subgenerics = subSymbol.GetFullyQualifiedGenericsString();

                    if (subgenerics.Length > 0)
                    {
                        genericStringComponents.Add(subgenerics);
                    }
                }
            }

            if (genericStringComponents.Count < 1)
            {
                return @base;
            }

            var joined = string.Join(", ", genericStringComponents);

            return $"{@base}<{joined}>";
        }

        public static bool IsEnumerable(this ITypeSymbol symbol)
        {
            return symbol.HasInterface(typeof(IEnumerable<>).Name);
        }

        public static bool IsPartiallyUdaptableClass(this ITypeSymbol symbol)
        {
            return symbol.TypeKind == TypeKind.Class
                && symbol.GetFullyQualifiedName() != "System.String"
                && !symbol.IsEnumerable();
        }

        public static bool HasInterface(this ITypeSymbol symbol, string name)
        {
            return symbol.AllInterfaces.Any(i => i.MetadataName == name);
        }

        public static string GetFullyQualifiedName(this ISymbol symbol)
        {
            return $"{symbol.ContainingNamespace}.{symbol.Name}";
        }

        public static bool HasAttribute(this ITypeSymbol typeSymbol, string name)
        {
            var attrs = typeSymbol.GetAttributes();
            foreach (var attr in attrs)
            {
                if (attr.AttributeClass?.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public static void AddStaticFile(this IncrementalGeneratorPostInitializationContext context, string filename)
        {
            var templateString = GetEmbedFileAsString(filename);

            var outputName = filename.Replace(".cs", ".generated.cs");

            context.AddSource(outputName, templateString);
        }

        public static void AddTemplate(this IncrementalGeneratorPostInitializationContext context, string filename, string discriminator, Dictionary<string, string> replacements)
        {
            var source = FillTemplateFromFile(filename, replacements);

            if (discriminator is not null)
            {
                filename = GetDiscriminatedName(filename, discriminator);
            }

            context.AddSource(filename, source);
        }

        public static void AddTemplate(this SourceProductionContext context, string filename, string discriminator, Dictionary<string, string> replacements)
        {
            var source = FillTemplateFromFile(filename, replacements);

            if (discriminator is not null)
            {
                filename = GetDiscriminatedName(filename, discriminator);
            }

            context.AddSource(filename, source);
        }

        public static string GetDiscriminatedName(string filename, string discriminator)
        {
            discriminator = string.Concat(discriminator.Split(Path.GetInvalidFileNameChars()));

            filename = filename.Replace(".cs", $".{discriminator}.cs");
            filename = filename.Replace(".cs", ".generated.cs");

            return filename;
        }
    }
}
