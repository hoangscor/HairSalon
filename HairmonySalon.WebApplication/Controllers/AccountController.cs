using HairHarmonySalon.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using BCrypt.Net;
using Harmony.Services.Interfaces;
using Harmony.Services;
using Harmony.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
namespace HairHarmonySalon.Controllers
{
	public class AccountController : AppController
	{
        HarmonySalonContext db = new HarmonySalonContext();
        // khai baos IUserService
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController()
        {

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            // Kiểm tra xem Email đã tồn tại hay chưa
            var existingUser = db.Users.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Email already exists.");
                return View(model);
            }

            // Tạo người dùng mới
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
				UserType = "Customer"
			};

            // Lưu người dùng vào cơ sở dữ liệu
            db.Users.Add(user);
            if (db.SaveChanges() > 0)
            {
				// Đăng kí account thành công => đăng nhập sẵn cho user
				var user_name = user.Name.ToString();
				HttpContext.Session.SetString("UserName", user_name);
				var role = user.UserType.ToString();
				HttpContext.Session.SetString("Role", role);
				var user_id = user.UserId.ToString();
				HttpContext.Session.SetString("UserId", user_id);
				// Chuyển hướng đến trang Home sau khi đăng ký thành công
				return RedirectToAction("Index", "Home");
			}

            // Trả về để báo validation
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}

