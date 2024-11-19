using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HairHarmonySalon.Models;

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
