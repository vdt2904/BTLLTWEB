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
        [Route("nhanvien")]
        public IActionResult NhanVien(int? page)
        {
            int pageSize = 15;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstnhanvien = db.NhanViens.AsNoTracking().OrderBy(x => x.TenNv);
            PagedList<NhanVien> lst = new PagedList<NhanVien>(lstnhanvien, pageNumber, pageSize);
            return View(lst);
        }

        //Thêm nhân viên
        [Route("ThemNhanvien")]
        [HttpGet]
        public IActionResult ThemNhanVien()
        {
            return View();
        }
        [Route("ThemNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNhanVien(NhanVien nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(nhanvien);
                db.SaveChanges();
                return RedirectToAction("NhanVien");
            }
            return View(nhanvien);
        }

        // Sửa nhân viên
        [Route("SuaNhanVien")]
        [HttpGet]
        public IActionResult SuaNhanVien(String manv)
        {
            var nhanvien = db.NhanViens.Find(manv);
            return View(nhanvien);
        }

        [Route("SuaNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNhanVien(NhanVien nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.Update(nhanvien);
                db.SaveChanges();
                return RedirectToAction("NhanVien", "HomeAdmin");
            }
            return View(nhanvien);
        }

        //Xóa nhân viên
        [Route("XoaNhanVien")]
        [HttpGet]
        public IActionResult XoaNhanVien(String manv)
        {
            TempData["Message"] = "";
            var nhanvien = db.HoaDons.Where(x => x.MaNv == manv).ToList();
            if (nhanvien.Count() > 0)
            {
                TempData["Message"] = "Không xóa được nhân viên này";
                return RedirectToAction("NhanVien", "Admin");
            }
            var nhanvien1 = db.Logins.Where(x => x.MaNv == manv);
            if (nhanvien1.Any()) db.RemoveRange(nhanvien1);
            var nhanvien2 = db.Blogs.Where(x => x.MaNv == manv);
            if (nhanvien2.Any()) db.RemoveRange(nhanvien2);
            db.Remove(db.NhanViens.Find(manv));
            db.SaveChanges();
            TempData["Message"] = "Nhân viên đã được xóa";
            return RedirectToAction("NhanVien", "Admin");
        }
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
        //them sua xoa SDTB begin!
        //hien thi SDTB begin!
        [Route("SDTB")]
        public IActionResult SDTB(int? page)
        {
            int pageSize = 15;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSDTB = db.SuDungThietBis.AsNoTracking().OrderBy(x => x.MaPhong);
            PagedList<SuDungThietBi> lst = new PagedList<SuDungThietBi>(lstSDTB, pageNumber, pageSize);
            return View(lst);
        }
        //hien thi SDTB end!
        //them SDTB begin!
        [Route("ThemSDTB")]
        [HttpGet]
        public IActionResult ThemSDTB()
        {
            ViewBag.MaPhong = new SelectList(db.Phongs.ToList(), "MaPhong", "TenPhong");
            ViewBag.MaTb = new SelectList(db.ThietBis.ToList(), "MaTb", "TenTb");
            return View();
        }
        [Route("ThemSDTB")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSDTB(SuDungThietBi sdthietbi)
        {
            if (ModelState.IsValid)
            {
                var existingThietBi = db.SuDungThietBis.Where(tb => tb.MaTb == sdthietbi.MaTb && tb.MaPhong == sdthietbi.MaPhong).FirstOrDefault();
                if (existingThietBi != null)
                {
                    existingThietBi.SoLuong += sdthietbi.SoLuong;
                }
                else
                {
                    db.SuDungThietBis.Add(sdthietbi);
                }                
                db.SaveChanges();
                return RedirectToAction("SDTB");
            }
            return View(sdthietbi);
        }
        //them SDTB end!
        //Xoa thiet bi begin!
        [Route("XoaSDThietBi")]
        [HttpGet]
        public IActionResult XoaSDThietBi(string maTb,string maPhong)
        {
            var sdtb = db.SuDungThietBis.FirstOrDefault(x => x.MaTb == maTb && x.MaPhong == maPhong);
            db.Remove(sdtb);
            db.SaveChanges();
            TempData["Message"] = "Loại Phòng đã được xóa";
            return RedirectToAction("SDTB", "HomeAdmin");
        }
        //Xoa thiet bi end!
        //them sua xoa SDTD end!

    }
}
