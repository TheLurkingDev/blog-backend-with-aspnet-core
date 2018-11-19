using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class BlogCategory
    {
        [Key]
        [Column("BlogCategoryID")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Information { get; set; }

        [ForeignKey("WebsiteID")]
        public Guid WebsiteID { get; set; }
    }
}
