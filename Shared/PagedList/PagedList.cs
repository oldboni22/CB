namespace Shared.PagedList;

public struct PagedList<T>(IList<T> Items, int PageNumber, int PageSize, int TotalCount, int TotalPageCount);