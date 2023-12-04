using System.Collections.Generic;

namespace TemplateNamespaceTemplate;

public static class TemplateClassTemplate
{
    public static IEnumerable<TEntity> TemplateMethodNameTemplate<TEntity>(this IEnumerable<TEntity> TemplateParameterNameTemplate) where TEntity : TemplateParameterTypeTemplate
    {
        foreach (var item in TemplateParameterNameTemplate)
        {
            item.TemplateMethodNameTemplate();
        }

        return TemplateParameterNameTemplate;
    }
}
