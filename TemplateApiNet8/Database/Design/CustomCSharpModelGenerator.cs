using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Scaffolding;
using System.Text.RegularExpressions;

namespace TemplateApiNet8.Database.Design;

// Magically found by EF vya reflection
public class CustomDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IModelCodeGenerator, CustomCSharpModelGenerator>();
    }
}

public class CustomCSharpModelGenerator : CSharpModelGenerator
{
    // (public partial class \w+)
    // |
    // (public Guid Id { get; set; })
    private static readonly Regex Regex = new Regex(@"(public partial class \w+)|(public Guid Id { get; set; })", RegexOptions.Compiled);
    private static readonly string BaseClassName = "BaseEntity";
    private static readonly string BaseClassInheritance = $": {BaseClassName}";
    private static readonly string BaseClass = $"public abstract class {BaseClassName} {{ public abstract Guid Id {{ get; set; }} }}";

    public CustomCSharpModelGenerator(ModelCodeGeneratorDependencies dependencies, IOperationReporter reporter, IServiceProvider serviceProvider) : base(dependencies, reporter, serviceProvider)
    {
    }
    
    public override ScaffoldedModel GenerateModel(IModel model, ModelCodeGenerationOptions options)
    {
        ScaffoldedModel? defaultModel = base.GenerateModel(model, options);
        var modelFiles = defaultModel.AdditionalFiles;

        for (int i = 0; i < modelFiles.Count; i++)
        {
            var modelFile = modelFiles[i];

            if (modelFile.Code.Contains("public Guid Id { get; set; }"))
            {
                var temp = Regex.Replace(modelFile.Code, evaluator =>
                {
                    if (evaluator.Value.StartsWith("public Guid"))
                    {
                        return "public override Guid Id { get; set; }";
                    }

                    if (evaluator.Value.StartsWith("public partial class"))
                    {
                        var value = evaluator.Value + BaseClassInheritance;
                        return value;
                    }

                    throw new Exception("This Shouldn't Have Happened, You Should Review The Custom Scaffolding Code Generator");
                });

                modelFile.Code = temp;
            }
        }

        var contextPath = defaultModel.ContextFile.Path;

        contextPath = Path.GetDirectoryName(contextPath);

        contextPath = Path.Combine(contextPath, $"{BaseClassName}.cs");

        modelFiles.Add(new ScaffoldedFile()
        {
            Code = BaseClass,
            Path = contextPath,
        });

        return defaultModel;
    }
}

