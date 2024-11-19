using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmony.Repositories.Service
{
    public class ApplicationUser : IdentityUser
    {
        public string FName { get; set; } = "";
        public string LName { get; set; } = "";
        public string Address { get; set; } = "";
        public DateTime CreateAt { get; set; }
    }
}
