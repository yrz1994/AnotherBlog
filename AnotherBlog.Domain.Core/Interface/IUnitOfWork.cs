using System.Threading.Tasks;

namespace AnotherBlog.Domain.Core.Interface
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
