using System.ComponentModel.DataAnnotations;

namespace AnotherBlog.IdentityServer.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Account { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
