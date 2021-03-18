using AnotherBlog.Domain.Core.Interface;
using AnotherBlog.Domain.Core.Model;
using AnotherBlog.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherBlog.Domain.Models
{
    [Table("Article", Schema = "blog")]
    public class Article : Entity, IAggregateRoot
    {
        [Required]
        public long ArticleNo { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; private set; }

        [Required]
        [MaxLength(64)]
        public string Author { get; private set; }

        [Required]
        [MaxLength(512)]
        public string Summary { get; private set; }

        public ArticleContent Content { get; set; }

        public ArticleType Type { get; private set; }

        [Required]
        public ArticleStatus Status { get; private set; }

        [Required]
        public DateTime CreateDateTime { get; private set; }

        public DateTime PostDateTime { get; private set; }

        public DateTime LastEditTime { get; private set; }
    }
}
