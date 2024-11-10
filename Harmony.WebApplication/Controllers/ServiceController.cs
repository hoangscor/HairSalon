using Microsoft.AspNetCore.Mvc;

namespace Harmony.WebApplication.Controllers
{
	public class ServiceController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Appointment()
		{
			return View();
		}
		public IActionResult PickTime()
		{
			return View();
		}

		public IActionResult SelectStylist()
		{
			return View();
		}

	}
}
