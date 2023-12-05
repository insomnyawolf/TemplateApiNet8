// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1024:Symbols should be compared for equality", Justification = "Documentation points that using == or equals(T) does the same and works as expected", Scope = "namespaceanddescendants", Target = "~N:CleanEntityFrameworkReferenceLoopsSourceGenerator")]
