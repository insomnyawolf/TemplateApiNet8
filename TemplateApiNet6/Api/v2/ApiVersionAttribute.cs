using Asp.Versioning;

namespace TemplateApiNet6.Api.v2;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class ApiV2Attribute : ApiVersionAttribute
{
    public static readonly ApiVersion Version = new(majorVersion: 2, minorVersion: 0, status: "Beta");
    public ApiV2Attribute() : base(version: Version) 
    {
        
    }
}
