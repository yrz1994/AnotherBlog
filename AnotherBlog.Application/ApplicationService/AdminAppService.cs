using AnotherBlog.Application.Interface;
using AnotherBlog.Application.Request;
using AnotherBlog.Application.Response;
using AnotherBlog.Domain.Core.Bus;
using AnotherBlog.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
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

        //public async Task<BaseResponse> AdmainLogin(AdminLoginRequest request)
        //{
        //    var adminInfo = await _bus.SendQuery(new GetAdmainByEmail(request.Email));
        //    if(adminInfo == null)
        //    {
        //    }
        //}

    }
}
