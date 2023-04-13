using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using BTL.Repository;
namespace BTL.ViewComponents
{
	public class SelectRoomViewComponent : ViewComponent
	{
		private readonly ILoaiPhongRepository _loaiPhongRepository;
		public SelectRoomViewComponent(ILoaiPhongRepository loaiPhongRepository)
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
