using Microsoft.AspNetCore.Mvc;
using Harmony.Repositories.Entities;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HairHarmonySalon.Controllers
{
	public class AppController : Controller
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);

			// Kiểm tra session ở đây
			ViewBag.user_session = "";
            if (HttpContext.Session.GetString("UserName") != null)
			{
				var user_session = HttpContext.Session.GetString("UserName");
				ViewBag.user_session = user_session;
			}

            ViewBag.user_role = "";
            if (HttpContext.Session.GetString("Role") != null)
            {
                var user_role = HttpContext.Session.GetString("Role");
                ViewBag.user_role = user_role;
            }
        }
	}
}