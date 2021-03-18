using AnotherBlog.Domain.Core.Bus.Handler;
using AnotherBlog.Domain.Core.Bus.Messages.Queries;
using AnotherBlog.Domain.Interface.User;
using AnotherBlog.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AnotherBlog.Domain.Queries
{
    public class GetAdmainByEmail : Query<Administrator>
    {
        public GetAdmainByEmail(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }

    public class GetAdmainByEmailHandler : QueryHandler, IRequestHandler<GetAdmainByEmail, Administrator>
    {
        private readonly IAdministratorRepository _administratorRepository;
        public GetAdmainByEmailHandler(IAdministratorRepository administratorRepository)
        {
            _administratorRepository = administratorRepository;
        }

        public async Task<Administrator> Handle(GetAdmainByEmail request, CancellationToken cancellationToken)
        {
            return await _administratorRepository.GetAdminByEmail(request.Email);
        }
    }
}
