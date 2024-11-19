using HairHarmonySalon.Models;
using Harmony.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HairHarmonySalon.Controllers
{
	public class HomeController : AppController
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService; // add read service
        public HomeController(IUserService userService, ILogger<HomeController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public IActionResult Index()
		{
			return View();
		}
		public IActionResult services()
		{
			return View();
		}
		public IActionResult Gallery()
		{
			return View();
		}
		public IActionResult About()
		{
			return View();
		}
		public IActionResult Blog()
		{
			return View();
		}
		public IActionResult Blog_Single()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}
		public IActionResult Privacy()
		{
			return View();
		}
		[AllowAnonymous]
		public IActionResult NonSecureMethod()
		{
			return View();
		}
		[Authorize]
		public IActionResult SecureMethod()
		{
			return View();
		}
		public async Task<IActionResult> Indexs()
        {
            var users = await _userService.Users();
            return View(users);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}
