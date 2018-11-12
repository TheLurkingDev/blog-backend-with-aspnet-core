using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Website
    {
        [Key]
        [Column("WID")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Website Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Website Url is required")]
        [Column("URL")]
        public string Url { get; set; }
    }
}
