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
        private readonly IIdentityServerInteractionService _interaction;

        public AccountController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
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

        /// <summary>
        /// 账号密码登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/[controller]/LoginAsync")]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Account))
            {
                return Json(new { isSuccess = false, message = "请输入用户名" });
            }
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                return Json(new { isSuccess = false, message = "请输入密码" });
            }
            var claim = new List<Claim>()
            {
                new Claim(type : "Role", value : "Admin")
            };
            var identityUser = new IdentityServerUser(model.Account)
            {
                DisplayName = model.Account,
                AdditionalClaims = claim
            };
            await HttpContext.SignInAsync(identityUser);
            var returnUrl = HttpContext.Request.Cookies[_returnUrlKey];
            return Json(new { isSuccess = true, returnUrl = returnUrl, message = "Success." });
        }
    }
}
