using BTL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static BTL.Areas.Admin.Controllers.HomeAdminController;

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
		public IActionResult DangXuat()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("Usernamekh");
			return RedirectToAction("Index", "Home");
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

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(DangKyViewModel model)
        {
            var a = model.LoginKh;
            var b = model.KhachHang;
            TempData["DKTB"] = "";
            var tk = db.LoginKhs.FirstOrDefault(x => x.Username == a.Username);
            if(tk != null) {
                TempData["DKTB"] = "Tên đăng nhập đã tồn tại";
            }else
            {
                var c = db.KhachHangs.Max(x => x.MaKh);
                string ma = maHdTd(c.ToString());
                KhachHang kh = new KhachHang{
                    MaKh = ma,
                    TenKh = b.TenKh,
                    Cccd = b.Cccd
                };
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                LoginKh lkh = new LoginKh
                {
                    MaKh = ma,
                    Username = a.Username,
                    Password = MD5Hash(a.Password)
                };
                db.LoginKhs.Add(lkh);
                db.SaveChanges();
                return RedirectToAction("DangNhap", "Access");
            }
            return View();
        }
        public string maHdTd(string a)
        {
            var c = db.KhachHangs.Max(x => x.MaKh);
            string maHD = a;
            Match match = Regex.Match(maHD, @"\d+");
            int soHD = int.Parse(match.Value);
            soHD++;
            string soHDString = soHD.ToString().PadLeft(match.Value.Length, '0');
            string maHDMoi = maHD.Substring(0, match.Index) + soHDString;
            return maHDMoi;
        }


    }
}
