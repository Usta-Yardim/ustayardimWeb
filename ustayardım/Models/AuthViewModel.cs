using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ustayardım.Models
{
    
        public class AuthViewModel
        {
            public LoginDTO LoginModel { get; set; } = null!;
            public RegisterDTO RegisterModel { get; set; } = null!;
            public string? ActionType { get; set; }  // Bu alan, hangi işlemin gerçekleştirildiğini belirtmek için kullanılabilir

        }
}