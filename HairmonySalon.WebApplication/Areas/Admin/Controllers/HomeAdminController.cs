using Harmony.Repositories.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HairHarmonySalon.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]

    public class HomeAdminController : Controller
    {
        HarmonySalonContext db = new HarmonySalonContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [Route("DanhMucUser")]
        public IActionResult DanhMucUser()
        {
            var lstUser = db.Users.ToList();
            return View(lstUser);
        }
    }
}
