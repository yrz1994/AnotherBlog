using AnotherBlog.Application.Request;
using AnotherBlog.Application.Response;
using System.Threading.Tasks;

namespace AnotherBlog.Application.Interface
{
    public interface IAdminAppService
    {
        Task<BaseResponse> AdmainLogin(AdminLoginRequest request);
    }
}
