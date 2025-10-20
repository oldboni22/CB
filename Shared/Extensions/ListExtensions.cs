using Shared.PagedList;

namespace Shared.Extensions;

public static class ListExtensions
{
    public static PagedList<T> ToPagedList<T>(this IList<T> items, int pageNumber, int pageSize, int totalCount, int totalPageCount) =>
     new PagedList<T>(items, pageNumber, pageSize, totalCount, totalPageCount);
    
}