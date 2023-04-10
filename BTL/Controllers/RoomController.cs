using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace BTL.Controllers
{
    public class RoomController : Controller
    {

		QlkhachSanAspContext db = new QlkhachSanAspContext();


		public IActionResult Index(int? page)
		{
			int pageSize = 3;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;

			var lstLoaiPhong = db.LoaiPhongs.AsNoTracking().OrderBy(x => x.MaLp);

			PagedList<LoaiPhong> lst = new PagedList<LoaiPhong>(lstLoaiPhong, pageNumber, pageSize);
			return View(lst);
		}

        public IActionResult PhongTheoLoai(string maLoai, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

			var phong = db.LoaiPhongs.Where(x => x.MaLp == maLoai).FirstOrDefault();
            ViewBag.Gia = phong.Gia;

            var lstPhong = db.Phongs.AsNoTracking().Where(x => x.MaLp == maLoai).OrderBy(x => x.MaLp);
            PagedList<Phong> lst = new PagedList<Phong>(lstPhong, pageNumber, pageSize);
            ViewBag.maloai = maLoai;
            return View(lst);
        }

        public IActionResult ChiTietPhong(string maphong, string maloai)
		{
			var Lphong = db.LoaiPhongs.SingleOrDefault(x => x.MaLp == maloai);

			var phong = db.Phongs.Where(x => x.MaPhong == maphong && x.MaLp == maloai).FirstOrDefault();

			ViewBag.TenPhong = phong.TenPhong;

			ViewBag.TinhTrang = phong.TinhTrang;

			var ctAnh = db.Ctanhs.Where(x => x.MaLp == maloai).ToList();
			ViewBag.ctAnh = ctAnh;

			return View(Lphong);
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
