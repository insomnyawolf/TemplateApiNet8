using Asp.Versioning;

namespace TemplateApiNet6.Api.v1;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class ApiV1Attribute : ApiVersionAttribute
{
    public static readonly ApiVersion Version = new(majorVersion: 1, minorVersion: 0, status: null);
    public ApiV1Attribute() : base(version: Version) 
    {
        
    }
}
