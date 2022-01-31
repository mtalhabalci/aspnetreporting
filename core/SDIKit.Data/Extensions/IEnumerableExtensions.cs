using SDIKit.Common.Interfaces;
using SDIKit.Data.Pagination;

using System;
using System.Collections.Generic;

namespace SDIKit.Data.Extensions
{
    public static class IEnumerablePagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom = 0)
        {
            return new PagedList<T>(source, pageIndex, pageSize, indexFrom);
        }

        public static IPagedList<TResult> ToPagedList<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int pageIndex, int pageSize, int indexFrom = 0)
        {
            return new PagedList<TSource, TResult>(source, converter, pageIndex, pageSize, indexFrom);
        }
    }
}