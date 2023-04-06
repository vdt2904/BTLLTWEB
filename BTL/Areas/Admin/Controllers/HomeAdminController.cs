using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace BTL.Areas.Admin.Controllers
{
	[Area("admin")]
	[Route("admin")]
	[Route("admin/homeadmin")]
	public class HomeAdminController : Controller
	{

		QlkhachSanAspContext db = new QlkhachSanAspContext();

		[Route("")]
		[Route("index")]
		public IActionResult Index()
		{

			return View();
		}
		//them sua xoa phong begin!
		//hien thi phong begin!
		[Route("phongks")]
		public IActionResult PhongKS(int? page)
		{
			int pageSize = 15;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstphong = db.Phongs.AsNoTracking().OrderBy(x => x.TenPhong);
			PagedList<Phong> lst = new PagedList<Phong>(lstphong, pageNumber, pageSize);
			return View(lst);
		}
        //hien thi phong end!
        //them phong begin!
        [Route("ThemPhong")]
        [HttpGet]
        public IActionResult ThemPhong()
        {
          ViewBag.MaLp = new SelectList(db.LoaiPhongs.ToList(), "MaLp", "LoaiPhong1");

            return View();
        }
        [Route("ThemPhong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemPhong(Phong phong)
        {
			if (ModelState.IsValid)
			{
                db.Phongs.Add(phong);
                db.SaveChanges();
                return RedirectToAction("PhongKS");
            }
            return View(phong);
        }
        //them phong end!
        //Sua phong begin!
        [Route("SuaPhong")]
        [HttpGet]
        public IActionResult SuaPhong(string maphong)
        {
            ViewBag.MaLp = new SelectList(db.LoaiPhongs.ToList(), "MaLp", "LoaiPhong1");
            var phong = db.Phongs.Find(maphong);
            return View();
        }
        [Route("SuaPhong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaPhong(Phong phong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PhongKS","HomeAdmin");
            }
            return View(phong);
        }
        //Sua phong end!
        //Xoa Phong begin!
        [Route("XoaPhong")]
        [HttpGet]
        public IActionResult XoaPhong(string maPhong)
        {
            TempData["Message"] = "";
            var DatPhongs = db.DatPhongs.Where(x => x.MaPhong == maPhong).ToList();
            if (DatPhongs.Count > 0)
            {
                TempData["Message"] = "Không thể xóa được phòng này";
                return RedirectToAction("PhongKS", "HomeAdmin");
            }
            var CTTB = db.SuDungThietBis.Where(x => x.MaPhong == maPhong).AsEnumerable();
            if (CTTB.Any())
            {
                string sql = "DELETE FROM SuDungThietBi WHERE MaPhong = {0}";
                db.Database.ExecuteSqlRaw(sql, maPhong);
            }
            db.Remove(db.Phongs.Find(maPhong));
            db.SaveChanges();
            TempData["Message"] = "Phòng đã được xóa";
            return RedirectToAction("PhongKS", "HomeAdmin");
        }
        //Xoa phong end!
        //them sua xoa phong end!
        //them sua xoa loaiphong begin!
        //hien thi loaiphong begin!
        [Route("LoaiPhong")]
        public IActionResult LoaiPhong(int? page)
        {
            int pageSize = 15;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstloaiphong = db.LoaiPhongs.AsNoTracking().OrderBy(x => x.LoaiPhong1);
            PagedList<LoaiPhong> lst = new PagedList<LoaiPhong>(lstloaiphong, pageNumber, pageSize);
            return View(lst);
        }
        //hien thi loaiphong end!
        //them loai phong begin!
        [Route("ThemLoaiPhong")]
        [HttpGet]
        public IActionResult ThemLoaiPhong()
        {
            return View();
        }
        [Route("ThemLoaiPhong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemLoaiPhong(LoaiPhong loaiphong)
        {
            if (ModelState.IsValid)
            {
                db.LoaiPhongs.Add(loaiphong);
                db.SaveChanges();
                return RedirectToAction("LoaiPhong");
            }
            return View(loaiphong);
        }
        //them loai phong end!
        //Sua loai phong begin!
        [Route("SuaLoaiPhong")]
        [HttpGet]
        public IActionResult SuaLoaiPhong(string malp)
        {
            var loaiphong = db.LoaiPhongs.Find(malp);
            return View();
        }
        [Route("SuaLoaiPhong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaLoaiPhong(LoaiPhong Loaiphong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Loaiphong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LoaiPhong", "HomeAdmin");
            }
            return View(Loaiphong);
        }
        //sua loai phong end!
        //Xoa loai Phong begin!
        [Route("XoaLoaiPhong")]
        [HttpGet]
        public IActionResult XoaLoaiPhong(string maLp)
        {
            TempData["Message"] = "";
            var Phongs = db.Phongs.Where(x => x.MaLp == maLp).ToList();
            if (Phongs.Count > 0)
            {
                TempData["Message"] = "Không thể xóa được loại phòng này";
                return RedirectToAction("LoaiPhong", "HomeAdmin");
            }
            db.Remove(db.LoaiPhongs.Find(maLp));
            db.SaveChanges();
            TempData["Message"] = "Loại Phòng đã được xóa";
            return RedirectToAction("LoaiPhong", "HomeAdmin");
        }
        //Xoa loai phong end!
        //them sua xoa loaiphong end!
        // them sua xoa thietbi begin!
        //hien thi ThietBi begin!
        [Route("ThietBi")]
        public IActionResult ThietBi(int? page)
        {
            int pageSize = 15;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstthietbi = db.ThietBis.AsNoTracking().OrderBy(x => x.TenTb);
            PagedList<ThietBi> lst = new PagedList<ThietBi>(lstthietbi, pageNumber, pageSize);
            return View(lst);
        }
        //hien thi ThietBi end!
        //them thiet bi begin!
        [Route("ThemThietBi")]
        [HttpGet]
        public IActionResult ThemThietBi()
        {
            return View();
        }
        [Route("ThemThietBi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemThietBi(ThietBi thietbi)
        {
            if (ModelState.IsValid)
            {
                db.ThietBis.Add(thietbi);
                db.SaveChanges();
                return RedirectToAction("ThietBi");
            }
            return View(thietbi);
        }
        //them thiet bi end!
        //Sua thiet bi begin!
        [Route("SuaThietBi")]
        [HttpGet]
        public IActionResult SuaThietBi(string maTb)
        {
            var thietbi = db.ThietBis.Find(maTb);
            return View();
        }
        [Route("SuaThietBi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaThietBi(ThietBi thietbi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thietbi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThietBi", "HomeAdmin");
            }
            return View(thietbi);
        }
        //sua thiet bi end!
        //Xoa thiet bi begin!
        [Route("XoaThietBi")]
        [HttpGet]
        public IActionResult XoaThietBi(string maTb)
        {
            TempData["Message"] = "";
            var sdthietBis = db.SuDungThietBis.Where(x => x.MaTb == maTb).ToList();
            if (sdthietBis.Count > 0)
            {
                TempData["Message"] = "Không thể xóa được loại phòng này";
                return RedirectToAction("ThietBi", "HomeAdmin");
            }
            db.Remove(db.ThietBis.Find(maTb));
            db.SaveChanges();
            TempData["Message"] = "Loại Phòng đã được xóa";
            return RedirectToAction("ThietBi", "HomeAdmin");
        }
        //Xoa thiet bi end!
        // them sua xoa thietbi end!
    }
}
