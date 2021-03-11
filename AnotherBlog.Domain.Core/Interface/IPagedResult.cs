using System.Collections.Generic;

namespace AnotherBlog.Domain.Core.Interface
{
    public interface IPagedResult
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
    }

    public interface IPagedResult<T> : IPagedResult
    {
        IEnumerable<T> Data { get; }
    }
}
