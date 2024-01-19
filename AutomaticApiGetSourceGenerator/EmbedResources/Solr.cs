using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using ApiGetGenerator;
using ApiGetGenerator.Models;
using SolrNet;
using SolrNet.Commands.Parameters;

namespace TemplateNamespaceTemplate;

public partial class TemplateControllerNameTemplate
{
    public partial async TemplateReturnStringTemplate TemplateGetEndpointMethodNameTemplate(TemplateParamsTemplate)
    {
        var pageSize = TemplateQueryParamNameTemplate?.PageSize ?? 10;
        var pageIndex = TemplateQueryParamNameTemplate?.PageIndex ?? 0;

        // Pagination settings
        var options = new QueryOptions()
        {
            Fields = new List<string>() { "*", "[child]" },
            Rows = pageSize,
            StartOrCursor = new StartOrCursor.Start(pageSize * pageIndex),
        };

        if (TemplateQueryParamNameTemplate is not null)
        {
            var orderBy = TemplateQueryParamNameTemplate.OrderBy;

            if (orderBy is not null)
            {
                var srt = new List<SortOrder>();

                for (int i = 0; i < orderBy.Count; i++)
                {
                    var current = orderBy[i];

                    var direction = current.Direction switch
                    {
                        OrderDirection.Ascending => Order.ASC,
                        OrderDirection.Descending => Order.DESC,
                    };

                    var fieldName = current.Column.ToString();

                    srt.Add(new SortOrder(fieldName, direction));
                }

                options.OrderBy = srt;
            }

            var fqs = new List<ISolrQuery>();

TemplateGetFiltersTemplate

            options.FilterQueries = fqs;
        }

        // By default if the query is empty it doesn't return anything
        // That's not a bad behaviour but i personally prefer having some data directly so i know if things are working properly
        //var defaultQuery = "*";
        //This should be faster since it needs less string checks
        var defaultQuery = "-_nest_path_:*";

        var q = await TemplateSolrClientVariableNameTemplate.QueryAsync(defaultQuery, options);

        // We use a custom class to return the pagination data because the SolrQueryResults doesn't serialize any extra fields, just the data
        return new(q, (int)q.NumFound, pageSize, pageIndex);
    }
}
