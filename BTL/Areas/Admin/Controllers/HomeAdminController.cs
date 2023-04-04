using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

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
