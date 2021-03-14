using AnotherBlog.Domain.Core.Bus.Messages.Commands;
using AnotherBlog.Domain.Core.Bus.Messages.Events;
using AnotherBlog.Domain.Core.Bus.Messages.Queries;
using FluentValidation.Results;
using MediatR;
using System.Threading.Tasks;

namespace AnotherBlog.Domain.Core.Bus
{
    public class MemoryBus : IMemoryBus
    {
        private readonly IMediator _mediator;

        public MemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await _mediator.Publish(@event);
        }

        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }

        public async Task<TResponse> SendQuery<T, TResponse>(T query) where T : Query<TResponse>
        {
            return await _mediator.Send(query);
        }
    }
}
