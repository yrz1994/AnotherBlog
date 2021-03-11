using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnotherBlog.Domain.Core.Interface
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> AddAsync(T obj);
        Task AddRangeAsync(IEnumerable<T> list);
        void Remove(T obj);
        void Update(T obj);
        Task<T> GetByIdAsync(long id);
        Task<IList<T>> GetAllAsync();
    }
}
