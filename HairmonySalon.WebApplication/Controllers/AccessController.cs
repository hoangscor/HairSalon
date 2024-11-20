using Microsoft.AspNetCore.Mvc;
using Harmony.Repositories.Entities;
namespace HairHarmonySalon.Controllers
{
    public class AccessController : AppController
    {
		HarmonySalonContext db = new HarmonySalonContext();
        [HttpGet]

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.Users.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(
                    user.Password)).FirstOrDefault();
                if (u != null)
                {
                    var user_name = u.Name.ToString();
					HttpContext.Session.SetString("UserName", user_name);

                    var role = u.UserType.ToString();
                    HttpContext.Session.SetString("Role", role);

                    var user_id = u.UserId.ToString();
                    HttpContext.Session.SetString("UserId", user_id);


                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("UserName");
			return RedirectToAction("Login", "Access");
		}
	}
}

