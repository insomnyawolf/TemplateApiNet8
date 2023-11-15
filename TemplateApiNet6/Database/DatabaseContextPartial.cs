﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq.Expressions;
using TemplateApiNet6.Database.Infraestructure;

namespace TemplateApiNet6.Database;

public partial class DatabaseContext : DbContext
{
    public bool IncludeSoftDeleted { get; set; }
    public int? TenantId { get; set; }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        var models = modelBuilder.Model.GetEntityTypes();

        var thisInstance = Expression.Constant(this);

        foreach (var model in models)
        {
            var clrType = model.ClrType;

            var expressionList = new List<BinaryExpression>();

            var dbItem = Expression.Parameter(clrType, clrType.Name);

            if (clrType.IsAssignableTo(typeof(ISoftDeleted)))
            {
                var includeSoftDeletedValues = Expression.Property(thisInstance, nameof(IncludeSoftDeleted));

                var falseExpression = Expression.Constant(false);
                var dbValue = Expression.Property(dbItem, nameof(ISoftDeleted.IsDeleted));
                var equalityExpression = Expression.Equal(dbValue, falseExpression);

                var includeSoftDeleted = Expression.OrElse(includeSoftDeletedValues, equalityExpression);
                expressionList.Add(includeSoftDeleted);
            }

            if (clrType.IsAssignableTo(typeof(IMultiTenant)))
            {
                var nullableWrapper = Expression.Property(thisInstance, nameof(TenantId));
                var filterValue = Expression.Property(nullableWrapper, "Value");
                var dbValue = Expression.Property(dbItem, nameof(IMultiTenant.TenantId));
                var equalityExpression = Expression.Equal(dbValue, filterValue);
                expressionList.Add(equalityExpression);
            }

            if (expressionList.Count < 1)
            {
                continue;
            }

            BinaryExpression body = expressionList[0];

            for (int i = 1; i < expressionList.Count; i++)
            {
                body = Expression.AndAlso(body, expressionList[i]);
            }

            var lambda = Expression.Lambda(body, dbItem);

            model.SetQueryFilter(lambda);
        }
    }
}
