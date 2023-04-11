using BTL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BTL.ViewComponents
{
	public class MenuDichVuViewComponent: ViewComponent
	{
		private readonly IDichVu dv;
		public MenuDichVuViewComponent (IDichVu dv)
		{
			this.dv = dv;
		}
		public IViewComponentResult Invoke()
		{
			return View(dv.GetAll().OrderBy(x => x.TenDv));
		}
	}
}
