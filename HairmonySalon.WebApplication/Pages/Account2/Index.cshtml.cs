using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Harmony.Repositories.Entities;

namespace HairHarmonySalon.Pages.Account2
{
    public class IndexModel : PageModel
    {
        private readonly Harmony.Repositories.Entities.HarmonySalonContext _context;

        public IndexModel(Harmony.Repositories.Entities.HarmonySalonContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
        }
    }
}
