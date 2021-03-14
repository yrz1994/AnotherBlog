using AnotherBlog.Domain.Core.Interface;
using AnotherBlog.Domain.Models;
using System.Threading.Tasks;

namespace AnotherBlog.Infra.Data.Interface.User
{
    public interface IAdministratorRepository : IRepository<Administrator>
    {
        Task<Administrator> GetAdminByEmail(string email);
    }
}
