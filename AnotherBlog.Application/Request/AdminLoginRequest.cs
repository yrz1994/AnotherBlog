using System.ComponentModel.DataAnnotations;

namespace AnotherBlog.Application.Request
{
    public class AdminLoginRequest
    {
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        [Required(ErrorMessage = "请输入邮箱地址")]
        public string Email { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
    }
}
