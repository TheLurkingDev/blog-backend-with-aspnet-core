using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("BlogPost")]
    public class BlogPost
    {
        [Key]
        [Column("PID")]
        public Guid Id { get; set; }

        public string Slug { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "DateCreated is required")]
        public DateTime DateCreated { get; set; }
    }
}
