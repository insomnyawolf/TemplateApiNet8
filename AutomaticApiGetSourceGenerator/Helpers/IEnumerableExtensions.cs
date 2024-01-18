using Microsoft.CodeAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SourceGeneratorHelpers;

public static class IEnumerableExtensions
{
    //public static IEnumerable<T> Distinct<T>(this IEnumerable<T> enumerable, Func<T, T, bool> comparison)
    //{
    //    return enumerable.Distinct(new LambdaEqualityComparison<T>(comparison));
    //}

    //internal class LambdaEqualityComparison<T> : IEqualityComparer<T>
    //{
    //    private readonly Func<T, T, bool> comparison;

    //    public LambdaEqualityComparison(Func<T, T, bool> comparison)
    //    {
    //        this.comparison = comparison;
    //    }

    //    public bool Equals(T x, T y)
    //    {
    //        return comparison.Invoke(x, y);
    //    }

    //    public int GetHashCode(T obj)
    //    {
    //        return 1;
    //    }
    //}

    public static IEnumerable<TEntity> Distinct<TEntity, TSelector>(this IEnumerable<TEntity> enumerable, Func<TEntity, TSelector> selector)
    {
        return enumerable.Distinct(new LambdaSelectorComparison<TEntity, TSelector>(selector));
    }

    public static object GetByName(this ImmutableArray<KeyValuePair<string, TypedConstant>> array, string argument)
    {
        var filtered = array.Where(a => a.Key == argument);
        var arg = filtered.FirstOrDefault();
        return arg.Value.Value!;
    }

    internal class LambdaSelectorComparison<TEntity, TSelector> : IEqualityComparer<TEntity>
    {
        private readonly Func<TEntity, TSelector> selector;

        public LambdaSelectorComparison(Func<TEntity, TSelector> selector)
        {
            this.selector = selector;
            
        }

        public bool Equals(TEntity x, TEntity y)
        {
            var a = selector(x);
            var b = selector(y);
            var equals = EqualityComparer<TSelector>.Default.Equals(a, b);
            return equals;
        }

        public int GetHashCode(TEntity obj)
        {
            return 1;
        }
    }
}