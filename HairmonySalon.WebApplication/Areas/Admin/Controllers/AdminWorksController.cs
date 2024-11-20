/*using HarmonySalon.Reponsitories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairHarmonySalon.Areas.Admin.Controllers
{
  
        [Area("Admin")]
        public class AdminServicesController : Controller
        {
            private readonly HarmonySalonContext _context;

        public AdminServicesController(HarmonySalonContext context)
            {
                _context = context;
            }

            // GET: Admin/AdminServices
            public async Task<IActionResult> Index()
            {
                var services = await _context.Services.ToListAsync(); // Sử dụng 'Services' thay vì 'Works'
                return View(services);
            }

            // GET: Admin/AdminServices/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null) return NotFound();

                var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
                if (service == null) return NotFound();

                return View(service);
            }

            // GET: Admin/AdminServices/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Admin/AdminServices/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Name,Duration,Price,Description,Category,Image")] Service service)
            {
                if (service == null) return View("Error");

                if (ModelState.IsValid)
                {
                    _context.Services.Add(service);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(service);
            }

            // GET: Admin/AdminServices/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null) return NotFound();

                var service = await _context.Services.FindAsync(id);
                if (service == null) return NotFound();

                return View(service);
            }

            // POST: Admin/AdminServices/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Duration,Price,Description,Category,Image")] Service service)
            {
                if (id != service.Id) return NotFound();

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Services.Update(service);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ServiceExists(service.Id)) return NotFound();
                        throw;
                    }

                    return RedirectToAction(nameof(Index));
                }

                return View(service);
            }

            // GET: Admin/AdminServices/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null) return NotFound();

                var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
                if (service == null) return NotFound();

                return View(service);
            }

            // POST: Admin/AdminServices/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var service = await _context.Services.FindAsync(id);
                if (service != null)
                {
                    _context.Services.Remove(service);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            private bool ServiceExists(int id)
            {
                return _context.Services.Any(s => s.Id == id); // Kiểm tra dịch vụ tồn tại
            }
        }
    }

}
}
*/