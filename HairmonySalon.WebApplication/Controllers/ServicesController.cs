using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HairHarmonySalon.Models;
using HairHarmonySalon.Models.Authentication;

namespace HairHarmonySalon.Controllers
{
	public class ServicesController : AppController
	{
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
			return View();
		}

		public IActionResult SelectStylist()
		{
			return View();
		}

	}
}
