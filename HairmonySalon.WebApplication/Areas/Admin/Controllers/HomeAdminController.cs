using HairHarmonySalon.ViewModel;
using Harmony.Repositories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

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
            // phân quyền 
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
            // xem trùng email
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
        //sua User
        [Route("SuaUser")]
        [HttpGet]
        public IActionResult SuaUser(int id)
        {
            // Tìm người dùng theo ID
            var user = db.Users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                // Nếu không tìm thấy người dùng, trả về lỗi
                ModelState.AddModelError("", "User not found.");
                return RedirectToAction("DanhMucUser", "admin");
            }
            ViewBag.UserId = user.UserId;

            return View();
        }
        [Route("SuaUser")]
        [HttpPost]
        public IActionResult SuaUser(User suaUser)
        {
            // Tìm người dùng theo ID
            var user = db.Users.FirstOrDefault(u => u.UserId == suaUser.UserId);

            if (user == null)
            {
                // Nếu không tìm thấy người dùng, trả về lỗi
                ModelState.AddModelError("", "User not found.");
                return View(suaUser);
            }

            // Kiểm tra xem email mới có trùng với email của người dùng khác không
            var existingUser = db.Users.FirstOrDefault(u => u.Email == suaUser.Email && u.UserId != user.UserId);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Email already exists for another user.");
                return View(suaUser);
            }

            // Cập nhật thông tin người dùng
            user.Name = suaUser.Name;
            user.Email = suaUser.Email;
            user.Password = suaUser.Password; 
            user.UserType = suaUser.UserType;
            // Lưu thay đổi vào cơ sở dữ liệu
            db.Update(user);
            db.SaveChanges();

            // Chuyển hướng về danh sách người dùng sau khi sửa thành công
            return RedirectToAction("DanhMucUser", "admin");
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

        //Sua Appointment
        [Route("SuaAppointment")]
        [HttpGet]
        public IActionResult SuaAppointment(int id)
        {
            var list_stylist = db.Users.Where(u => u.UserType == "Stylist").ToList();
            var list_customer = db.Users.Where(u => u.UserType == "Customer").ToList();
            var list_service = db.Services.ToList();

            ViewBag.list_stylist = list_stylist;
            ViewBag.list_customer = list_customer;
            ViewBag.list_service = list_service;
            var appointment = db.Appointments.FirstOrDefault(a => a.AppointmentId == id);
            if (appointment == null)
            {
                // Nếu không tìm thấy appointment, trả về lỗi
                ModelState.AddModelError("", "Appointment not found.");
                return RedirectToAction("DanhMucAppointment", "admin");
            }
            ViewBag.appointment = appointment;

            //datetime
            string result_date = "";
            if (appointment.AppointmentDate != "")
            {
                string str_date = appointment.AppointmentDate;
                string[] parts = str_date.Split(' ');

                // Chuyển đổi sang định dạng 24 giờ
                DateTime time24Hour = DateTime.ParseExact(parts[0], "h:mmtt", CultureInfo.InvariantCulture);
                // Định dạng lại thời gian thành HH:mm (24 giờ)
                result_date = time24Hour.ToString("HH:mm");
                result_date = parts[1] + "T" + result_date;
            }
            ViewBag.date_time = result_date;

            return View();
        }

        [Route("SuaAppointment")]
        [HttpPost]
        public IActionResult SuaAppointment(Appointment appointments)
        {
            var appointment = db.Appointments.FirstOrDefault(a => a.AppointmentId == appointments.AppointmentId);

            if (appointment == null)
            {
                // Nếu không tìm thấy người dùng, trả về lỗi
                ModelState.AddModelError("", "User not found.");
                return RedirectToAction("DanhMucAppointment", "admin");
            }

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

            appointment.CustomerId = appointments.CustomerId;
            appointment.StylistId = appointments.StylistId;
            appointment.ServiceId = appointments.ServiceId;
            appointment.AppointmentDate = string_date;
            appointment.Status = appointments.Status;

            db.Update(appointment);
            var result = db.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("DanhMucAppointment", "admin");
            }

            return View(appointment);
        }
    }
}
