using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ustayardım.Models
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public bool RememberMe { get; set;} = true;
    }
}