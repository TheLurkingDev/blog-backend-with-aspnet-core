using SecurityService;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class User
    {
        [Key]
        [Column("UserID")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public UserRole Role { get; set; }

        [Required(ErrorMessage = "DateCreated is required")]
        public DateTime DateCreated { get; set; }

        public byte[] HashedPassword { get; set; }
        public byte[] Salt { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        [NotMapped]
        public string Password { get; set; }        
    }
}
