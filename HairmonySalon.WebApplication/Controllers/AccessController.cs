using Microsoft.AspNetCore.Mvc;
using Harmony.Repositories.Entities;
using HarmonySalon.Reponsitories.Entities;
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
                    HttpContext.Session.SetString("UserName", u.Email.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}
