using AnotherBlog.Application.Interface;
using AnotherBlog.Application.Request;
using AnotherBlog.Application.Response;
using AnotherBlog.Domain.Core.Bus;
using AnotherBlog.Domain.Models;
using AnotherBlog.Domain.Queries;
using AnotherBlog.Infra.Utility;
using System.Threading.Tasks;

namespace AnotherBlog.Application.ApplicationService
{
    public class AdminAppService : IAdminAppService
    {
        private readonly IMemoryBus _bus;

        public AdminAppService(IMemoryBus bus)
        {
            _bus = bus;
        }

        public async Task<BaseResponse> AdmainLogin(AdminLoginRequest request)
        {
            var adminInfo = await _bus.SendQuery<GetAdmainByEmail, Administrator> (new GetAdmainByEmail(request.Email));
            if (adminInfo == null || !string.Equals(request.Password.GetMd5Hash(), adminInfo.Password))
            {
                return new BaseResponse(System.Net.HttpStatusCode.NotFound, "用户名或密码不正确");
            }
            return BaseResponse.Success();
        }
    }
}
