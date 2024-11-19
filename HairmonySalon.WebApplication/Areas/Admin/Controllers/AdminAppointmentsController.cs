using HarmonySalon.Reponsitories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HairHarmonySalon.Areas.Admin.Controllers
{
        [Area("Admin")]
        public class AdminAppointmentsController : Controller
        {
            private readonly HarmonySalonContext _context; // Use your actual context name

            public AdminAppointmentsController(HarmonySalonContext context)
            {
                _context = context;
            }

            // GET: Admin/AdminAppointments
            public async Task<IActionResult> Index()
            {
                var appointments = _context.Appointments
                    .Include(a => a.Customer)
                    .Include(a => a.Stylist)
                    .Include(a => a.Service)
                    .ToListAsync();
                return View(await appointments);
            }

            // GET: Admin/AdminAppointments/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var appointment = await _context.Appointments
                    .Include(a => a.Customer)
                    .Include(a => a.Stylist)
                    .Include(a => a.Service)
                    .FirstOrDefaultAsync(m => m.AppointmentId == id);

                if (appointment == null)
                {
                    return NotFound();
                }

                return View(appointment);
            }

            [HttpGet]
            public async Task<IActionResult> GetAppointments()
            {
                var appointments = await _context.Appointments
                    .Include(a => a.Service) // Include the service for the appointment
                    .Select(a => new
                    {
                        title = a.Service.Name,  // Assuming Service has a Name property
                        start = a.AppointmentDate,
                        end = a.AppointmentDate.AddHours(1), // Assuming 1 hour duration for now
                        id = a.AppointmentId
                    })
                    .ToListAsync();

                return Json(appointments);
            }

            // GET: Admin/AdminAppointments/Create
            public IActionResult Create()
            {
                ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FullName"); // Assuming Customer has FullName
                ViewData["StylistId"] = new SelectList(_context.Stylists, "StylistId", "FullName"); // Assuming Stylist has FullName
                ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "Name"); // Assuming Service has Name
                return View();
            }

            // POST: Admin/AdminAppointments/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("AppointmentId,AppointmentDate,Status,CustomerId,StylistId,ServiceId")] Appointment appointment)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(appointment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FullName", appointment.CustomerId);
                ViewData["StylistId"] = new SelectList(_context.Stylists, "StylistId", "FullName", appointment.StylistId);
                ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "Name", appointment.ServiceId);
                return View(appointment);
            }

            // GET: Admin/AdminAppointments/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }

                ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FullName", appointment.CustomerId);
                ViewData["StylistId"] = new SelectList(_context.Stylists, "StylistId", "FullName", appointment.StylistId);
                ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "Name", appointment.ServiceId);
                return View(appointment);
            }

            // POST: Admin/AdminAppointments/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,AppointmentDate,Status,CustomerId,StylistId,ServiceId")] Appointment appointment)
            {
                if (id != appointment.AppointmentId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(appointment);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AppointmentExists(appointment.AppointmentId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FullName", appointment.CustomerId);
                ViewData["StylistId"] = new SelectList(_context.Stylists, "StylistId", "FullName", appointment.StylistId);
                ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "Name", appointment.ServiceId);
                return View(appointment);
            }

            // GET: Admin/AdminAppointments/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var appointment = await _context.Appointments
                    .Include(a => a.Customer)
                    .Include(a => a.Stylist)
                    .Include(a => a.Service)
                    .FirstOrDefaultAsync(m => m.AppointmentId == id);
                if (appointment == null)
                {
                    return NotFound();
                }

                return View(appointment);
            }

            // POST: Admin/AdminAppointments/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment != null)
                {
                    _context.Appointments.Remove(appointment);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            private bool AppointmentExists(int id)
            {
                return _context.Appointments.Any(e => e.AppointmentId == id);
            }
        }
    }

