using AnotherBlog.Domain.Core.Interface;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace AnotherBlog.Domain.Core.CommandHandler
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<ValidationResult> Commit(IUnitOfWork uow, string errMsg)
        {
            if (!await uow.Commit()) AddError(errMsg);

            return ValidationResult;
        }

        protected async Task<ValidationResult> Commit(IUnitOfWork uow)
        {
            return await Commit(uow, "Commit failed.").ConfigureAwait(false);
        }
    }
}
