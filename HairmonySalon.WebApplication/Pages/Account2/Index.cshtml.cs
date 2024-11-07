using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HarmonySalon.Reponsitories.Entities;

namespace HairHarmonySalon.Pages.Account2
{
    public class IndexModel : PageModel
    {
        private readonly HarmonySalon.Reponsitories.Entities.HarmonySalonContext _context;

        public IndexModel(HarmonySalon.Reponsitories.Entities.HarmonySalonContext context)
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
