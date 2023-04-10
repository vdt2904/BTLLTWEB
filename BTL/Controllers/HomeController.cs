using BTL.Models;
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

        public IActionResult Index(int? page)
        {
            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstLoaiPhong = db.LoaiPhongs.AsNoTracking().OrderBy(x => x.Gia);

            PagedList<LoaiPhong> lst = new PagedList<LoaiPhong>(lstLoaiPhong, pageNumber, pageSize);
            return View(lst);
        }

		public IActionResult CheckRoom()
		{
			var lstLoaiPhong = db.LoaiPhongs.AsNoTracking().OrderBy(x => x.Gia);
			return View(lstLoaiPhong);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}