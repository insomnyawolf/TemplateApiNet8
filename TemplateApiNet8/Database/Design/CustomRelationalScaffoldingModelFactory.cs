using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;

namespace TemplateApiNet8.Database.Design
{
    public class CustomRelationalScaffoldingModelFactory : RelationalScaffoldingModelFactory
    {
        public CustomRelationalScaffoldingModelFactory(IOperationReporter reporter, ICandidateNamingService candidateNamingService, IPluralizer pluralizer, ICSharpUtilities cSharpUtilities, IScaffoldingTypeMapper scaffoldingTypeMapper, IModelRuntimeInitializer modelRuntimeInitializer) : base(reporter, candidateNamingService, pluralizer, cSharpUtilities, scaffoldingTypeMapper, modelRuntimeInitializer)
        {
        }
    }
}
