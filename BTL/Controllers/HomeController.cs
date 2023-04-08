using BTL.Models;
using BTL.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace BTL.Controllers
{
	public class HomeController : Controller
	{
        QlkhachSanAspContext db = new QlkhachSanAspContext();

        private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstLoaiPhong = db.LoaiPhongs.AsNoTracking().OrderBy(x => x.Gia);

            PagedList<LoaiPhong> lst = new PagedList<LoaiPhong>(lstLoaiPhong, pageNumber, pageSize);
            return View(lst);
        }
        [Authentication]
        public IActionResult Room(int? page)
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            //var room = db.LoaiPhongs.Include("TenPhong").Include("TinhTrang").ToList();
            var lstLoaiPhong = db.LoaiPhongs.AsNoTracking().OrderBy(x => x.MaLp);

            PagedList<LoaiPhong> lst = new PagedList<LoaiPhong>(lstLoaiPhong, pageNumber, pageSize);
            return View(lst);
        }
        [Authentication]
        public IActionResult PhongTheoLoai(string maLoai, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstphong = db.LoaiPhongs.AsNoTracking().Where(x => x.MaLp == maLoai).OrderBy(x => x.Gia);
            PagedList<LoaiPhong> lst = new PagedList<LoaiPhong>(lstphong, pageNumber, pageSize);
            ViewBag.maloai = maLoai;
            return View(lst);
        }
        [Authentication]
        public IActionResult ChiTietPhong(string maloai)
        {
            //var room = db.LoaiPhongs.Include("TenPhong").Include("TinhTrang").ToList();

            var room = db.LoaiPhongs.SingleOrDefault(x => x.MaLp == maloai);
            var anhPhong = db.LoaiPhongs.Where(x => x.MaLp == maloai).ToList();
            ViewBag.anhPhong = anhPhong;
            return View(room);
        }
        [Authentication]
        public IActionResult Privacy()
		{
			return View();
		}
        [Authentication]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}