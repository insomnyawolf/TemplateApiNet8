using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace TemplateApiNet8.Database.Design;

// Planned, Scaffolding Costomizations Like Adding Base Class

public class CustomRelationalScaffoldingModelFactory : RelationalScaffoldingModelFactory
{
    public CustomRelationalScaffoldingModelFactory(IOperationReporter reporter, ICandidateNamingService candidateNamingService, IPluralizer pluralizer, ICSharpUtilities cSharpUtilities, IScaffoldingTypeMapper scaffoldingTypeMapper, IModelRuntimeInitializer modelRuntimeInitializer) : base(reporter, candidateNamingService, pluralizer, cSharpUtilities, scaffoldingTypeMapper, modelRuntimeInitializer)
    {
    }
}
