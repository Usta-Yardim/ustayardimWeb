using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ustayardım.Models
{
    public class RegisterDTO 
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Key]
        [Required] 
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string UserType { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}