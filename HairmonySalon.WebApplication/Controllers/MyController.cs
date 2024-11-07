using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HairHarmonySalon.Controllers
{
	public class MyController : Controller
	{
			private readonly UserManager<IdentityUser> _userManager;

			public MyController(UserManager<IdentityUser> userManager)
			{
				_userManager = userManager;
			}
		private readonly RoleManager<IdentityRole> _roleManager;

		public MyController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}
	}
}
