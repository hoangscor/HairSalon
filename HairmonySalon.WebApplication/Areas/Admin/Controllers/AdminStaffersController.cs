/*using HarmonySalon.Reponsitories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HairHarmonySalon.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSalonStaffController : Controller
    {
        private readonly HarmonySalonContext _context;

        public AdminSalonStaffController(HarmonySalonContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminSalonStaff
        public async Task<IActionResult> Index()
        {
            var staffList = await _context.SalonStaffs
                .Include(s => s.Staff) // Liên kết tới thông tin User
                .ToListAsync();

            return View(staffList);
        }

        // GET: Admin/AdminSalonStaff/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var salonStaff = await _context.SalonStaffs
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.StaffId == id);

            if (salonStaff == null) return NotFound();

            return View(salonStaff);
        }

        // GET: Admin/AdminSalonStaff/Create
        public IActionResult Create()
        {
            ViewData["StaffId"] = new SelectList(_context.Users, "UserId", "Name");
            return View();
        }

        // POST: Admin/AdminSalonStaff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,Availability")] SalonStaff salonStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salonStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffId"] = new SelectList(_context.Users, "UserId", "Name", salonStaff.StaffId);
            return View(salonStaff);
        }

        // GET: Admin/AdminSalonStaff/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var salonStaff = await _context.SalonStaffs.FindAsync(id);
            if (salonStaff == null) return NotFound();

            ViewData["StaffId"] = new SelectList(_context.Users, "UserId", "Name", salonStaff.StaffId);
            return View(salonStaff);
        }

        // POST: Admin/AdminSalonStaff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,Availability")] SalonStaff salonStaff)
        {
            if (id != salonStaff.StaffId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salonStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalonStaffExists(salonStaff.StaffId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffId"] = new SelectList(_context.Users, "UserId", "Name", salonStaff.StaffId);
            return View(salonStaff);
        }

        // GET: Admin/AdminSalonStaff/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var salonStaff = await _context.SalonStaffs
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.StaffId == id);

            if (salonStaff == null) return NotFound();

            return View(salonStaff);
        }

        // POST: Admin/AdminSalonStaff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salonStaff = await _context.SalonStaffs.FindAsync(id);
            if (salonStaff != null)
            {
                _context.SalonStaffs.Remove(salonStaff);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SalonStaffExists(int id)
        {
            return _context.SalonStaffs.Any(e => e.StaffId == id);
        }
    }
}
*/