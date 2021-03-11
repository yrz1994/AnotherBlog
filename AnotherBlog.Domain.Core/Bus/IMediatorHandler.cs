using AnotherBlog.Domain.Core.Messages.Commands;
using AnotherBlog.Domain.Core.Messages.Events;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace AnotherBlog.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;

        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
