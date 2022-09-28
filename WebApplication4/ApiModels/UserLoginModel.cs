using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.ApiModels
{
    public class UserLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string password { get; set; }
    }
}
