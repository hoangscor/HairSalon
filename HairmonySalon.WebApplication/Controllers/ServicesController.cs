using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HairHarmonySalon.Models;
using HairHarmonySalon.Models.Authentication;
using Harmony.Repositories.Entities;
using HairHarmonySalon.ViewModel;

namespace HairHarmonySalon.Controllers
{
	public class ServicesController : AppController
	{
        HarmonySalonContext db = new HarmonySalonContext();
        public IActionResult Index()
		{
			return View();
		}
		public IActionResult Appointment()
		{
			return View();
		}

		[Authentication]
		public IActionResult PickTime()
		{
            var listStylist = db.Users.Where(u => u.UserType == "Stylist").ToList();
            var listService = db.Services.ToList();
            ViewBag.listStylist = listStylist;
            ViewBag.listService = listService;
            return View();
		}
        [Authentication]
        [HttpPost]
        public async Task<IActionResult> PickTime(Appointment model)
        {
            var user_id = HttpContext.Session.GetString("UserId");
            var appointment = new Appointment
            {
                CustomerId = int.Parse(user_id),
                StylistId = model.StylistId,
                ServiceId = model.ServiceId,
                AppointmentDate = model.AppointmentDate,
            };

            db.Appointments.Add(appointment);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SelectStylist()
		{
			return View();
		}

	}
}
