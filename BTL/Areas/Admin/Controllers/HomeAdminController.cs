using BTL.Areas.Admin.Models;
using BTL.Areas.Admin.Models.Authentication;
using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BTL.Areas.Admin.Controllers
{
	[Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    [Authentication]
    public class HomeAdminController : Controller
	{
        QlkhachSanAspContext db = new QlkhachSanAspContext();

		[Route("")]
		[Route("index")]
		public IActionResult Index()
		{
            //csvc
            DateTime date = DateTime.Now;   
            
            ViewBag.Years = new SelectList(db.Csvcs.Select(x => x.Nam).Distinct().OrderByDescending(x => x).ToList(), date.Year);
            
            //doanhthu
            ViewBag.Yearss = new SelectList(db.TDoanhThus.Select(x => x.Nam).Distinct().OrderByDescending(x => x).ToList(), date.Year);
            
            return View();

        }
        [Route("Index")]
        [HttpPost]
         public IActionResult Index(int year)
         {
             var viewModel = new MyViewModel();

             //csvc
             ViewBag.Years = new SelectList(db.Csvcs.Select(x => x.Nam).Distinct().OrderByDescending(x => x).ToList(), year);
             viewModel.Csvcs = db.Csvcs
                 .Where(x => x.Nam == year)
                 .OrderBy(x => x.Thang)
                 .Select(x => new BarChartViewModel { Labels = new List<string> { "Tháng: " + x.Thang.ToString() }, Data = new List<double?> { x.TongTien } })
                 .ToList();
             double totalMoney = db.Csvcs.Where(x => x.Nam == year).Sum(x => x.TongTien) ?? 0;
             viewModel.TongTien = totalMoney.ToString("N0");

             //doanhthu
             ViewBag.Yearss = new SelectList(db.TDoanhThus.Select(x => x.Nam).Distinct().OrderByDescending(x => x).ToList(), year);
             viewModel.TDoanhThus = db.TDoanhThus
                 .Where(x => x.Nam == year)
                 .OrderBy(x => x.Thang)
                 .Select(x => new BarChartViewModel { Labels = new List<string> { "Tháng: " + x.Thang.ToString() }, Data = new List<double?> { x.TongTien } })
                 .ToList();
             double totalDT = db.TDoanhThus.Where(x => x.Nam == year).Sum(x => x.TongTien) ?? 0;
             viewModel.DoanhThu = totalDT.ToString("N0");

             return View(viewModel);
         }
        public class MyViewModel
        {
            public List<BarChartViewModel> Csvcs { get; set; }
            public List<BarChartViewModel> TDoanhThus { get; set; }
            public string TongTien { get; set; }
            public string DoanhThu { get; set; }
        }
        //them sua xoa phong begin!
        //hien thi phong begin!
        [Route("phongks")]
		public IActionResult PhongKS()
		{

			return View();
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
            TempData["Message"] = "";
            if (ModelState.IsValid)
			{
                TempData["Message"] = "Mã phòng đã có";
                var sphong = db.Phongs.Where(x => x.MaPhong == phong.MaPhong).FirstOrDefault();
                if(sphong != null)
                {
                    return View(phong);
                }
                db.Phongs.Add(phong);
                db.SaveChanges();
                return RedirectToAction("PhongKS");
            }
            ViewBag.MaLp = new SelectList(db.LoaiPhongs.ToList(), "MaLp", "LoaiPhong1");
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
            return View(phong);
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
            TempData["Message"] = "";
            if (ModelState.IsValid)
            {
                TempData["Message"] = "Mã loai phòng đã có";
                var sLphong = db.LoaiPhongs.Where(x => x.MaLp == loaiphong.MaLp).FirstOrDefault();
                if (sLphong != null)
                {
                    return View(loaiphong);
                }
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
            return View(loaiphong);
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
            int pageSize = 6;
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
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            return View();
        }
        [Route("ThemNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNhanVien(NhanVien nhanvien)
        {
            TempData["Message"] = "";
            if (ModelState.IsValid)
            {
                if (db.NhanViens.Any(x => x.MaNv == nhanvien.MaNv))
                {
                    TempData["Message"] = "Mã nhân viên đã tồn tại";
                    return RedirectToAction("ThemNhanVien", "Admin");
                }
                db.NhanViens.Add(nhanvien);
                db.SaveChanges();
                return RedirectToAction("NhanVien");
            }
            return View(nhanvien);
        }

        // Sửa nhân viên
        [Route("SuaNhanVien")]
        [HttpGet]
        public IActionResult SuaNhanVien(string manv)
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
        public IActionResult XoaNhanVien(string manv)
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
            TempData["Message"] = "";
            if (ModelState.IsValid)
            {
                TempData["Message"] = "Mã thiết bị đã có";
                var snv = db.ThietBis.Where(x => x.MaTb == thietbi.MaTb).FirstOrDefault();
                if (snv != null)
                {
                    return View(thietbi);
                }
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
            return View(thietbi);
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
                TempData["Message"] = "Không thể xóa được Thiet bi này";
                return RedirectToAction("ThietBi", "HomeAdmin");
            }
            db.Remove(db.ThietBis.Find(maTb));
            db.SaveChanges();
            TempData["Message"] = "Thiet bi đã được xóa";
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
            ViewBag.MaPhong = new SelectList(db.Phongs.ToList(), "MaPhong", "TenPhong");
            ViewBag.MaTb = new SelectList(db.ThietBis.ToList(), "MaTb", "TenTb");
            if (ModelState.IsValid)
            {
                TempData["Message"] = "";
                var existingThietBi = db.SuDungThietBis.Where(tb => tb.MaTb == sdthietbi.MaTb && tb.MaPhong == sdthietbi.MaPhong).FirstOrDefault();
                if (existingThietBi != null)
                {
                    ViewBag.MaPhong = new SelectList(db.Phongs.ToList(), "MaPhong", "TenPhong");
                    ViewBag.MaTb = new SelectList(db.ThietBis.ToList(), "MaTb", "TenTb");
                    TempData["Message"] = "Không thể thêm";
                    return View(sdthietbi);
                }
                else
                {
                    if (sdthietbi.NgaySD != null)
                    {
                        DateTime date = (DateTime)sdthietbi.NgaySD;
                        int nam = date.Year;
                        int thang = date.Month;
                        var csvc = db.Csvcs.FirstOrDefault(c => c.Nam == nam && c.Thang == thang);
                        if (csvc == null)
                        {
                            db.Csvcs.Add(new Csvc { Nam = nam, Thang = thang, TongTien = 0 });
                            db.SaveChanges();
                        }
                    }
                    db.SuDungThietBis.Add(sdthietbi);
                }                
                db.SaveChanges();
                return RedirectToAction("SDTB");
            }
            return View(sdthietbi);
        }
        //them SDTB end!
        //Sua thiet bi begin!
        [Route("SuaSDTB")]
        [HttpGet]
        public IActionResult SuaSDTB(string maTb,string maPhong)
        {
            var thietbi = db.SuDungThietBis.FirstOrDefault(x => x.MaTb == maTb && x.MaPhong == maPhong);
            return View(thietbi);
        }
        [Route("SuaSDTB")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSDTB(SuDungThietBi thietbi)
        {
            if (ModelState.IsValid)
            {
       //         db.Entry(thietbi).State = EntityState.Modified;
                db.Database.ExecuteSqlInterpolated($"update sudungthietbi set soluong = {thietbi.SoLuong}, tinhtrang = {thietbi.TinhTrang} where matb = {thietbi.MaTb} and maphong = {thietbi.MaPhong} ");
                db.SaveChanges();
                return RedirectToAction("SDTB", "HomeAdmin");
            }
            return View(thietbi);
        }
        //sua thiet bi end!
        //Xoa thiet bi begin!
        [Route("XoaSDThietBi")]
        [HttpGet]
        public IActionResult XoaSDThietBi(string maTb,string maPhong)
        {
            string sql = "DELETE FROM SuDungThietBi WHERE MaPhong = {0} and MaTB = {1}";
            db.Database.ExecuteSqlRaw(sql, maPhong,maTb);
            db.SaveChanges();
            return RedirectToAction("SDTB", "HomeAdmin");

        }
        //Xoa thiet bi end!
        //them sua xoa SDTD end!
        //them sua xoa dich vu begin!
        //hien thi dichvu begin!
        [Route("DichVu")]
        public IActionResult DichVu(int? page)
        {
            int pageSize = 15;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstdichvu = db.DichVus.AsNoTracking().OrderBy(x => x.MaDv);
            PagedList<DichVu> lst = new PagedList<DichVu>(lstdichvu, pageNumber, pageSize);
            return View(lst);
        }
        //hien thi dichvu end!
        //them ThemDichVu begin!
        [Route("ThemDichVu")]
        [HttpGet]
        public IActionResult ThemDichVu()
        {
            return View();
        }
        [Route("ThemDichVu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemDichVu(DichVu dv)
        {
            TempData["Message"] = "";
            if (ModelState.IsValid)
            {
                TempData["Message"] = "Mã dịch vụ đã có";
                var snv = db.DichVus.Where(x => x.MaDv == dv.MaDv).FirstOrDefault();
                if (snv != null)
                {
                    return View(dv);
                }
                db.DichVus.Add(dv);
                db.SaveChanges();
                return RedirectToAction("DichVu");
            }
            return View(dv);
        }
        //them ThemDichVu end!
        //Sua dichvu begin!
        [Route("SuaDichVu")]
        [HttpGet]
        public IActionResult SuaDichVu(string maDv)
        {
            var dichvu = db.DichVus.Find(maDv);
            return View(dichvu);
        }
        [Route("SuaDichVu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaDichVu(DichVu dichvu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dichvu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DichVu", "HomeAdmin");
            }
            return View(dichvu);
        }
        //sua dichvu end!
        //Xoa dichvu begin!
        [Route("XoaDichVu")]
        [HttpGet]
        public IActionResult XoaDichVu(string maDv)
        {
            TempData["Message"] = "";
            var sdv = db.SuDungDichVus.Where(x => x.MaDv == maDv).ToList();
            if (sdv.Count > 0)
            {
                TempData["Message"] = "Không thể xóa được dịch vụ này";
                return RedirectToAction("DichVu", "HomeAdmin");
            }
            db.Remove(db.DichVus.Find(maDv));
            db.SaveChanges();
            TempData["Message"] = "Dịch vụ đã được xóa";
            return RedirectToAction("DichVu", "HomeAdmin");
        }
        //Xoa dichvu end!
        //them sua xoa dich vu end!


        //Hóa Đơn
        [Route("HoaDon")]
        public IActionResult HoaDon(int? page)
        {
            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lsthoadon = db.HoaDons.AsNoTracking();
            PagedList<HoaDon> lst = new PagedList<HoaDon>(lsthoadon, pageNumber, pageSize);
            return View(lst);
        }

        //Sửa hóa đơn
        [Route("SuaHoaDon")]
        [HttpGet]
        public IActionResult SuaHoaDon(string mahd)
        {
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "MaNv");
            ViewBag.MaKh = new SelectList(db.KhachHangs.ToList(), "MaKh", "MaKh");
            var hoadon = db.HoaDons.Find(mahd);
            return View(hoadon);
        }

        [Route("SuaHoaDon")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaHoaDon(HoaDon hoadon)
        {
            if (ModelState.IsValid)
            {
                db.Update(hoadon);
                db.SaveChanges();
                return RedirectToAction("HoaDon", "HomeAdmin");
            }
            return View(hoadon);
        }

        //Xóa hóa đơn


        [Route("XoaHoaDon")]
        [HttpGet]
        public IActionResult XoaHoaDon(string mahd)
        {
            TempData["Message"] = "";
            var chitietdatphong = db.DatPhongs.Where(x => x.SoHoaDon == mahd).ToList();
            if (chitietdatphong.Count() > 0)
            {
                TempData["Message"] = "Không xóa được hóa đơn này";
                return RedirectToAction("HoaDon", "Admin");
            }
            var hoadon = db.HoaDons.Where(x => x.SoHoaDon == mahd);
            if (hoadon.Any()) db.RemoveRange(hoadon);
            db.Remove(db.HoaDons.Find(mahd));
            db.SaveChanges();
            TempData["Message"] = "Hóa đơn này đã được xóa";
            return RedirectToAction("HoaDon", "Admin");
        }

        // Khách Hàng
        [Route("KhachHang")]
        public IActionResult KhachHang(int? page)
        {
            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstkhachhang = db.KhachHangs.AsNoTracking().OrderBy(x =>x.MaKh);
            PagedList<KhachHang> lst = new PagedList<KhachHang>(lstkhachhang, pageNumber, pageSize);
            return View(lst);
        }

        //Sửa khách hàng
        [Route("SuaKhachHang")]
        [HttpGet]
        public IActionResult SuaKhachHang(string makh)
        {
            var khachhang = db.KhachHangs.Find(makh);
            return View(khachhang);
        }

        [Route("SuaKhachHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaKhachHang(KhachHang khachhang)
        {
            if (ModelState.IsValid)
            {
                db.Update(khachhang);
                db.SaveChanges();
                return RedirectToAction("KhachHang", "Admin");
            }
            return View(khachhang);
        }

        //Xóa khách hàng

        [Route("XoaKhachHang")]
        [HttpGet]
        public IActionResult XoaKhachHang(string makh)
        {
            TempData["Message"] = "";
            var chitiethoadon = db.HoaDons.Where(x => x.MaKh == makh);
                if (chitiethoadon.Count() >0)
                {
                    TempData["Message"] = "Không xóa được khách hàng này";
                    return RedirectToAction("KhachHang", "Admin");
                }
            var khachhang = db.KhachHangs.Where(x => x.MaKh == makh);
            if (khachhang.Any()) db.RemoveRange(khachhang);
            db.Remove(db.KhachHangs.Find(makh));
            db.SaveChanges();
            TempData["Message"] = "Thông tin khách hàng đã được xóa";
            return RedirectToAction("KhachHang", "Admin");
        }

        // Đặt phòng
        [Route("ThongTinDatPhong")]
        public IActionResult ThongTinDatPhong(int? page)
        {
            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstdatphong = db.DatPhongs.AsNoTracking().OrderBy(x => x.MaPhong);
            PagedList<DatPhong> lst = new PagedList<DatPhong>(lstdatphong, pageNumber, pageSize);
            return View(lst);
        }

        //Sửa phòng

        [Route("SuaTTDatPhong")]
        [HttpGet]
        public IActionResult SuaTTDatPhong(string maphong, string sohd)
        {
            var datphong = db.DatPhongs.FirstOrDefault(x => x.MaPhong == maphong && x.SoHoaDon == sohd);
            return View(datphong);
        }

        [Route("SuaTTDatPhong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTTDatPhong(DatPhong datphong)
        {
            if (ModelState.IsValid)
            {
                db.Update(datphong);
                db.SaveChanges();
                return RedirectToAction("ThongTinDatPhong", "Admin");
            }
            return View(datphong);
        }

        // Xóa thông tin đặt phòng
        [Route("XoaTTDatPhong")]
        [HttpGet]
        public IActionResult XoaTTDatPhong(string maphong, string sohd)
        {
            TempData["Message"] = "";
            string sql = "Delete from DatPhong where MaPhong = {0} and SoHoaDon = {1}";
            db.Database.ExecuteSqlRaw(sql,maphong,sohd);
            db.SaveChanges();
            TempData["Message"] = "Phòng đặt đã được xóa đã được xóa";
            return RedirectToAction("ThongTinDatPhong", "Admin");
        }
    }
}
