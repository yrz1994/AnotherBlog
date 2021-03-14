using AnotherBlog.Domain.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnotherBlog.Infra.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        protected readonly DbContext Db;
        protected readonly DbSet<T> DbSet;

        public IUnitOfWork UnitOfWork => Db as IUnitOfWork;

        public Repository(DbContext db)
        {
            Db = db;
            DbSet = Db.Set<T>();
        }

        public async Task<T> AddAsync(T obj)
        {
            var result = await DbSet.AddAsync(obj);
            return result.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> list)
        {
            await DbSet.AddRangeAsync(list);
        }

        public void Remove(T obj)
        {
            DbSet.Remove(obj);
        }

        public void Update(T obj)
        {
            DbSet.Update(obj);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }

    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection serviceCollection)
        {
            const string endsWith = "Repository";
            var repositorys = typeof(Repository<>).Assembly.GetTypes().Where(t => t.Name.EndsWith(endsWith) && t.GetInterfaces().Any(i => i.Name.EndsWith(endsWith))).ToList();
            if (repositorys.Count > 0)
            {
                foreach (var (item, i) in from item in repositorys from i in item.GetInterfaces() select (item, i))
                {
                    if (i.Name.EndsWith(endsWith))
                    {
                        serviceCollection.AddScoped(i, item);
                    }
                }
            }
            return serviceCollection;
        }
    }
}
