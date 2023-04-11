using BTL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Security.Cryptography;
using System.Text;

namespace BTL.Controllers
{
    public class AccessController : Controller
    {
        QlkhachSanAspContext db = new QlkhachSanAspContext();
        [HttpGet]
        public IActionResult DangNhap()
        {
            if (HttpContext.Session.GetString("Usernamekh") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult DangNhap(LoginKh login)
        {
            TempData["name"] = "";
            if (HttpContext.Session.GetString("Usernamekh") == null)
            {
                string pass = "";
                pass = MD5Hash(login.Password);
                var u = db.LoginKhs.Where(x => x.Username == login.Username && x.Password == pass).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("Usernamekh", u.Username.ToString());
                    TempData["name"] = HttpContext.Session.GetString("Usernamekh");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(login);
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
