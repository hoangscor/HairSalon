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
    public class DetailsModel : PageModel
    {
        private readonly HarmonySalon.Reponsitories.Entities.HarmonySalonContext _context;

        public DetailsModel(HarmonySalon.Reponsitories.Entities.HarmonySalonContext context)
        {
            _context = context;
        }

        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }
    }
}
