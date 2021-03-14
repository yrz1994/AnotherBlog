using AnotherBlog.Domain.Core.Bus.Messages.Events;

namespace AnotherBlog.Domain.Core.Interface
{
    public interface IAggregateRoot
    {
        void AddDomainEvent(Event domainEvent);
    }
}
