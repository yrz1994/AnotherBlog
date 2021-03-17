using AnotherBlog.Domain.Interface.User;
using AnotherBlog.Domain.Models;
using AnotherBlog.Infra.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AnotherBlog.Infra.Data.Repository.User
{
    public class AdministratorRepository : Repository<Administrator>, IAdministratorRepository
    {
        public AdministratorRepository(UserContext context) : base(context)
        {
        }

        public async Task<Administrator> GetAdminByEmail(string email)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
