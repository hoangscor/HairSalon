using HairHarmonySalon.ViewModel;
using Harmony.Repositories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace HairHarmonySalon.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]

    public class HomeAdminController : Controller
    {
        HarmonySalonContext db = new HarmonySalonContext();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin" && role != "Stylist" && role != "Manager")
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }


        [Route("")]
        [Route("index")]
        // index
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin" && role != "Stylist" && role != "Manager" )
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        // User
        [Route("DanhMucUser")]
        public IActionResult DanhMucUser()
        {
            var lstUser = db.Users.ToList();
            return View(lstUser);
        }
        //Them user
        [Route("ThemUserMoi")]
        [HttpGet]
        public IActionResult ThemUserMoi()
        {
            return View();
        }

        [Route("ThemUserMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemUserMoi(RegisterVM Users)
        {

            var existingUser = db.Users.FirstOrDefault(u => u.Email == Users.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Email already exists.");
                return View(Users);
            }

            var user = new User
            {
                Name = Users.Name,
                Email = Users.Email,
                Password = Users.Password,
                UserType = Users.UserType
            };
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("DanhMucUser", "admin");
            return View(Users);
        }
        // appointment
        [Route("DanhMucAppointment")]
		public IActionResult DanhMucAppointment()
		{
            var lstAppointment = db.Appointments
            .Include(a => a.Service) // Include bảng Service
            .Include(a => a.Stylist) // Include bảng User (Stylist)
            .Include(a => a.Customer) // Include bảng User (Customer)
            .ToList();

            return View(lstAppointment);
		}
        // them appointment
        [Route("ThemDanhMucAppointment")]
        [HttpGet]
        public IActionResult ThemDanhMucAppointment()
        {
            var list_stylist = db.Users.Where(u => u.UserType == "Stylist").ToList();
            var list_customer = db.Users.Where(u => u.UserType == "Customer").ToList();
            var list_service = db.Services.ToList();

            ViewBag.list_stylist = list_stylist;   
            ViewBag.list_customer = list_customer;   
            ViewBag.list_service = list_service;

            return View();
        }

        [Route("ThemDanhMucAppointment")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemDanhMucAppointment(Appointment appointments)
        {
            var date_time = appointments.AppointmentDate;
            string[] parts = date_time.Split('T');
            TimeSpan timeToCompare = TimeSpan.Parse(parts[1]);
            TimeSpan noon = TimeSpan.Parse("12:00");

            var string_date = "";
            if (timeToCompare > noon)
            {
                TimeSpan difference = timeToCompare - noon;
                string_date = difference.ToString(@"hh\:mm") + "PM" + " " + parts[0];
            }
            else
            {
                string_date = parts[1] + "AM" + " " + parts[0];
            }

            var _Appointment = new Appointment
            {
                CustomerId = appointments.CustomerId,
                StylistId = appointments.StylistId,
                ServiceId = appointments.ServiceId,
                AppointmentDate = string_date,
                Status = appointments.Status,
            };
            db.Appointments.Add(_Appointment);
            if (db.SaveChanges() > 0)
            {
                return RedirectToAction("DanhMucAppointment", "admin");
            }
            
            return View(_Appointment);
        }
    }
}
