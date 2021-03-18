using AnotherBlog.Domain.Core.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherBlog.Domain.Models
{
    public class ArticleContent : ValueObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        public string Content { get; private set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
