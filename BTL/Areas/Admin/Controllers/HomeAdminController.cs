using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTL.Areas.Admin.Controllers
{
	[Area("admin")]
	[Route("admin")]
	[Route("admin/homeadmin")]
	public class HomeAdminController : Controller
	{
		
		QL db = new QlkhachSanAspContext();
		[Route("")]
		[Route("index")]



		public IActionResult Index()
		{

			return View();
		}
		
	}
}
