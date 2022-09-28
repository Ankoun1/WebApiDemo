using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication4.Data
{
    public class AplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}
