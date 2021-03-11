using AnotherBlog.Domain.Core.Messages.Events;

namespace AnotherBlog.Domain.Core.Interface
{
    public interface IAggregateRoot
    {
        void AddDomainEvent(Event domainEvent);
    }
}
