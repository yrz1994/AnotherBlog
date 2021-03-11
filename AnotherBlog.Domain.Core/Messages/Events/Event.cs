using MediatR;

namespace AnotherBlog.Domain.Core.Messages.Events
{
    public abstract class Event : Message, INotification
    {

    }
}
