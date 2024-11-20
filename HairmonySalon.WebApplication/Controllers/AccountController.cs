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
            db.SaveChanges();

            // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
            /*return RedirectToAction("Login", "Account");*/
            return View(model);
        }



        [HttpGet]
        public IActionResult Login(string? ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    return View(model);
                }

                // Kiểm tra mật khẩu
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    return View(model);
                }

                // Đăng nhập thành công
                await signInManager.SignInAsync(user, isPersistent: model.RememberMe);
                return RedirectToAction("Index", "Home");
            }

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

