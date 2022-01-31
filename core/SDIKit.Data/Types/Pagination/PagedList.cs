using SDIKit.Common.Interfaces;

using System;
using System.Collections.Generic;

namespace SDIKit.Data.Pagination
{
    public static class PagedList
    {
        public static IPagedList<T> Empty<T>()
        {
            return new PagedList<T>();
        }

        public static IPagedList<TResult> From<TResult, TSource>(IPagedList<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            return new PagedList<TSource, TResult>(source, converter);
        }
    }
}