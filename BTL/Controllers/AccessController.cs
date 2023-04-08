using Microsoft.AspNetCore.Mvc;
using BTL.Models;

namespace BTL.Controllers
{
    public class AccessController : Controller
    {
        QlkhachSanAspContext db = new QlkhachSanAspContext();
        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("Username")==null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(Login user)
        {
            if(HttpContext.Session.GetString("Username")==null)
            {
                var u = db.Logins.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if(u!=null)
                {
                    HttpContext.Session.SetString("Username", u.Username.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Usernam");
            return RedirectToAction("Login", "Access");
        }
    }
}
