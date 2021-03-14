using AnotherBlog.Domain.Core.Bus.Messages.Commands;
using AnotherBlog.Domain.Core.Bus.Messages.Events;
using AnotherBlog.Domain.Core.Bus.Messages.Queries;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace AnotherBlog.Domain.Core.Bus
{
    public interface IMemoryBus
    {
        Task PublishEvent<T>(T @event) where T : Event;

        Task<ValidationResult> SendCommand<T>(T command) where T : Command;

        Task<TResponse> SendQuery<T, TResponse>(T query) where T : Query<TResponse>;
    }
}
