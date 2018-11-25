using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("BlogPost")]
    public class BlogPost
    {
        [Key]
        [Column("BlogPostID")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Slug is required")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "DateCreated is required")]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "LikeCount is required")]
        public Int16 LikeCount { get; set; }

        public Guid BlogCategoryID { get; set; }
    }
}
