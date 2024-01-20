﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using ApiGetGenerator;
using ApiGetGenerator.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TemplateNamespaceTemplate;

public partial class TemplateControllerNameTemplate
{
    public partial async TemplateReturnStringTemplate TemplateGetEndpointMethodNameTemplate(TemplateParamsTemplate)
    {
        var set = TemplateDbContextVariableNameTemplate.Set<TemplateDatabaseClassNameTemplate>().AsQueryable();

        var pageSize = TemplateQueryParamNameTemplate?.PageSize ?? 10;
        var pageIndex = TemplateQueryParamNameTemplate?.PageIndex ?? 0;

        set = set.Skip(pageSize * pageIndex);
        set = set.Take(pageSize);

        if (TemplateQueryParamNameTemplate is not null)
        {
            var includes = TemplateQueryParamNameTemplate.Includes;

            if (includes?.Count > 0 == true)
            {
                for (int includesIndex = 1; includesIndex < includes.Count; includesIndex++)
                {
                    var current = includes[includesIndex];

                    set = current switch
                    {
TemplateIncludeTemplate
                    };
                }
            }

            var orderBy = TemplateQueryParamNameTemplate.OrderBy;
            var orderByCount = orderBy?.Count;

            if (orderByCount > 0 == true)
            {
                IOrderedQueryable<TemplateDatabaseClassNameTemplate> orderedQueryable;

                var first = orderBy[0];

                if (first.Direction == OrderDirection.Descending)
                {
                    orderedQueryable = first.Column switch
                    {
TemplateOrderByTemplate
                    };
                }
                else
                {
                    orderedQueryable = first.Column switch
                    {
TemplateOrderByDescendingTemplate
                    };
                }

                for (int OrderByIndex = 1; OrderByIndex < orderByCount; OrderByIndex++)
                {
                    var current = orderBy[OrderByIndex];

                    if (current.Direction == OrderDirection.Descending)
                    {
                        orderedQueryable = current.Column switch
                        {
TemplateThenOrderByTemplate
                        };
                    }
                    else
                    {
                        orderedQueryable = current.Column switch
                        {
TemplateThenOrderByDescendingTemplate
                        };
                    }
                }

                set = orderedQueryable;
            }

TemplateGetFiltersTemplate
        }

        var count = await set.CountAsync();

        return new(set, count, pageSize, pageIndex);
    }
}
