namespace ApiGetGenerator;

[System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class GenerateGetAttribute : Attribute
{
    // This is a named argument
    public string InyectedDatabaseContextName { get; set; }
}