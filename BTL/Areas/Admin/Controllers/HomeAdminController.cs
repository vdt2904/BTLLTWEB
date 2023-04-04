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
        //them sua xoa phong end!
    }
}
