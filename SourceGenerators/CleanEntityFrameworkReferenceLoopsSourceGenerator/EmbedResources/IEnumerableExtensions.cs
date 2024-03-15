using System;
using System.Collections.Generic;

namespace TemplateNamespaceTemplate;

public static class TemplateClassTemplate
{
    public static IEnumerable<TEntity> TemplateMethodNameTemplate<TEntity>(this IEnumerable<TEntity> items) where TEntity : TemplateBaseClassTemplate
    {
        var TemplateParameterNameTemplate = new TemplateParameterTypeTemplate();
        foreach (var item in items)
        {
            TemplateParameterNameTemplate.Add(item.Id);
            item.TemplateMethodNameTemplate(TemplateParameterNameTemplate);
        }

        return items;
    }
}
