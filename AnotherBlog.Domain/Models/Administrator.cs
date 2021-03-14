using AnotherBlog.Domain.Core.Interface;
using AnotherBlog.Domain.Core.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherBlog.Domain.Models
{
    [Table("Administrator", Schema = "identity")]
    public class Administrator : Entity, IAggregateRoot
    {
        public Administrator() { }

        [MaxLength(16)]
        public string Name { get; private set; }

        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; private set; }

        [MaxLength(32)]
        public string Password { get; private set; }
    }
}
