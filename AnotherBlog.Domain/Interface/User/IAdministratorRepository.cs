using AnotherBlog.Domain.Core.Interface;
using AnotherBlog.Domain.Models;
using System.Threading.Tasks;

namespace AnotherBlog.Domain.Interface.User
{
    public interface IAdministratorRepository : IRepository<Administrator>
    {
        Task<Administrator> GetAdminByEmail(string email);
    }
}
