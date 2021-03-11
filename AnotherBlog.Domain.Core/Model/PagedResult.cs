using AnotherBlog.Domain.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnotherBlog.Domain.Core.Model
{
    [Serializable]
    public class PagedResult<T> : IPagedResult<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public IEnumerable<T> Data { get; private set; }
        public PagedResult(IPagedResult paged, T[] data)
        {
            this.Data = data;
            this.PageIndex = paged.PageIndex;
            this.PageSize = paged.PageSize;
            this.TotalCount = paged.TotalCount;
            this.TotalPages = paged.TotalPages;
        }

        public PagedResult(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int totalCount = source.Count();
            TotalCount = totalCount;
            TotalPages = totalCount / pageSize;
            if (totalCount % pageSize > 0)
            {
                TotalPages++;
            }
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = source.Skip(pageIndex * pageSize).Take(pageSize);
        }
    }
}
