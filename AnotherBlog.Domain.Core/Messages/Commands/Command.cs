using FluentValidation.Results;
using MediatR;

namespace AnotherBlog.Domain.Core.Messages.Commands
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public abstract bool IsValid();

        public ValidationResult ValidationResult { get; set; }
    }
}
