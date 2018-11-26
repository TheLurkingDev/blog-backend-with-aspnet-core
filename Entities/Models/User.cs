using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class User
    {
        [Key]
        [Column("UserID")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "HashedPassword is required")]
        public byte[] HashedPassword { get; set; }

        [Required(ErrorMessage = "Salt is required")]
        public byte[] Salt { get; set; }

        [Required(ErrorMessage = "DateCreated is required")]
        public DateTime DateCreated { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        [NotMapped]
        public string Password { get; set; }

        public Guid WebsiteID { get; set; }
    }
}
