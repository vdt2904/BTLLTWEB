using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using BTL.Repository;
namespace BTL.ViewComponents
{
	public class LoaiPhongHomeMenuViewComponent : ViewComponent
	{
		private readonly ILoaiPhongRepository _loaiPhongRepository;
		public LoaiPhongHomeMenuViewComponent(ILoaiPhongRepository loaiPhongRepository)
		{
			_loaiPhongRepository = loaiPhongRepository;	
		}
		public IViewComponentResult Invoke()
		{
			var loaiPhong = _loaiPhongRepository.GetAll().OrderBy(x => x.LoaiPhong1);
			return View(loaiPhong);
		}
	}
}
