
using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace BTL.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin1")]
    [Route("admin1/homeaccess")]

    public class HomeAccessController : Controller
    {
     
        QlkhachSanAspContext db = new QlkhachSanAspContext();
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "HomeAdmin");
            }

            
        }
        [Route("Index")]
        [HttpPost]
        public IActionResult Index(Login login)
        {
            TempData["name"] = "";
            if (HttpContext.Session.GetString("Username") == null)
            {
                string pass = "";
                pass = MD5Hash(login.Password);
                var u = db.Logins.Where(x => x.Username==login.Username && x.Password==pass).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("Username", u.Username.ToString());
                    TempData["name"] = HttpContext.Session.GetString("Username");
                    return RedirectToAction("Index", "HomeAdmin");
                }
                
            }
            
            return View(login);
        }
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "HomeAccess");
        }

        private string MD5Hash(string input)
        {
            using (MD5 md5hash = MD5.Create())
            {
                byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}
