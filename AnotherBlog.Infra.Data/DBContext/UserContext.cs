using AnotherBlog.Domain.Core.Bus;
using AnotherBlog.Domain.Core.Bus.Messages.Events;
using AnotherBlog.Domain.Core.Interface;
using AnotherBlog.Domain.Core.Model;
using AnotherBlog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AnotherBlog.Infra.Data.DBContext
{
    public class UserContext : DbContext, IUnitOfWork
    {
        private readonly IMemoryBus _memoryBus;

        public UserContext() { }

        public UserContext(DbContextOptions<UserContext> options, IMemoryBus memoryBus) : base(options)
        {
            _memoryBus = memoryBus;
        }

        public DbSet<Administrator> Administrator { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            var result = await SaveChangesAsync() > 0;
            await _memoryBus.PublishDomainEvents(this).ConfigureAwait(false);
            return result;
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublishDomainEvents<T>(this IMemoryBus memoryBus, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker.Entries<Entity>().Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

            domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents.Select(async (domainEvent) =>
            {
                await memoryBus.PublishEvent(domainEvent);
            });
            await Task.WhenAll(tasks);
        }
    }
}
