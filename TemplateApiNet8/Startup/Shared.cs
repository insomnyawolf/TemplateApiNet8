using Microsoft.Extensions.Configuration;

namespace TemplateApiNet8.Startup;

public static class Shared
{
    public static TSettings GetCurrent<TSettings>(this IConfiguration IConfiguration, string currentSection)
    {
        var parentGroup = IConfiguration.GetSection(currentSection);
        
        var settingsType = typeof(TSettings);

        var currentGroup = parentGroup.GetSection(settingsType.Name);

        var settings = currentGroup.Get<TSettings>();

        return settings;
    }
}
