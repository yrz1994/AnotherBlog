using MediatR;

namespace AnotherBlog.Domain.Core.Bus.Messages.Queries
{
    public class Query<T> : Message, IRequest<T>
    {
    }
}
