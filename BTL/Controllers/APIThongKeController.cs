using BTL.Areas.Admin.Models;
using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static BTL.Areas.Admin.Controllers.HomeAdminController;

namespace BTL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class APIThongKeController : Controller
	{
		private readonly QlkhachSanAspContext db = new QlkhachSanAspContext();
		[HttpGet]
		[Route("thongke")]
		public IActionResult GetAllThongKe()
		{
			DateTime date = DateTime.Now;
			var viewModel = new MyViewModel();
			//csvc
			
			viewModel.Csvcs = db.Csvcs
				.Where(x => x.Nam == date.Year)
				.OrderBy(x => x.Thang)
				.Select(x => new BarChartViewModel { Labels = new List<string> { "Tháng: " + x.Thang.ToString() }, Data = new List<double?> { x.TongTien } })
				.ToList();
			double totalMoney = db.Csvcs.Where(x => x.Nam == date.Year).Sum(x => x.TongTien) ?? 0;
			viewModel.TongTien = totalMoney.ToString("N0");
			//doanhthu
			
			viewModel.TDoanhThus = db.TDoanhThus
				.Where(x => x.Nam == date.Year)
				.OrderBy(x => x.Thang)
				.Select(x => new BarChartViewModel { Labels = new List<string> { "Tháng: " + x.Thang.ToString() }, Data = new List<double?> { x.TongTien } })
				.ToList();
			double totalDT = db.TDoanhThus.Where(x => x.Nam == date.Year).Sum(x => x.TongTien) ?? 0;
			viewModel.DoanhThu = totalDT.ToString("N0");

			return Ok(viewModel);
		}
	}
}