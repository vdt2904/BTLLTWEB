using Microsoft.AspNetCore.Mvc;

namespace BTL.Areas.Admin.Controllers
{
	public class HomeAdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
