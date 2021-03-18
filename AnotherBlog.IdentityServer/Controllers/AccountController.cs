using AnotherBlog.Application.Interface;
using AnotherBlog.Application.Request;
using AnotherBlog.Application.Response;
using AnotherBlog.IdentityServer.ViewModels;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnotherBlog.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _returnUrlKey = "return_url";
        private readonly IAdminAppService _adminAppService;
        private readonly IIdentityServerInteractionService _interaction;

        public AccountController(IIdentityServerInteractionService interaction, IAdminAppService adminAppService)
        {
            _interaction = interaction;
            _adminAppService = adminAppService;
        }

        public IActionResult Login(string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl)) returnUrl = "/";
            Response.Cookies.Append(_returnUrlKey, returnUrl, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1)
            });
            return View();
        }

        [HttpPost]
        [Route("/Account/LoginAsync")]
        public async Task<IActionResult> LoginAsync(AdminLoginRequest request)
        {
            var loginResult = await _adminAppService.AdmainLogin(request);
            if (loginResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return Ok(loginResult);
            }
            var claim = new List<Claim>()
            {
                new Claim(type : "Role", value : "Admin")
            };
            var identityUser = new IdentityServerUser(request.Email)
            {
                DisplayName = request.Email.Split("@")[0],
                AdditionalClaims = claim
            };
            await HttpContext.SignInAsync(identityUser);
            return Ok(DataResponse<string>.Success(HttpContext.Request.Cookies[_returnUrlKey]));
        }

        public async Task<IActionResult> Logout(string logoutId)
        {
            var context = await _interaction.GetLogoutContextAsync(logoutId);
            var vm = new LogoutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = context?.PostLogoutRedirectUri,
                SignOutIframeUrl = context?.SignOutIFrameUrl,
                LogoutId = logoutId
            };
            await HttpContext.SignOutAsync();
            return View(vm);
        }
    }
}
