using MediatR;

namespace AnotherBlog.Domain.Core.Bus.Messages.Events
{
    public abstract class Event : Message, INotification
    {

    }
}
