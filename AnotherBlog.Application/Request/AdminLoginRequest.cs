using System.ComponentModel.DataAnnotations;

namespace AnotherBlog.Application.Request
{
    public class AdminLoginRequest
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password")]
        public string Password { get; set; }
    }
}
