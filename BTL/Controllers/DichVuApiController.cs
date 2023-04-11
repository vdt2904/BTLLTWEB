using BTL.Models;
using BTL.Models.DichVuF1;
//using BTL.Models.RoomModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DichVuApiController : ControllerBase
	{
		QlkhachSanAspContext db=new QlkhachSanAspContext();
		[HttpGet("{madv}")]
		public IEnumerable<DichVuF1> Get(string madv)
		{

			var divu = from p in db.DichVus
					   where p.MaDv==madv
					   select new DichVuF1()
					   {
						   MaDv=p.MaDv,
						   Anh=p.Anh,
						   TenDv=p.TenDv,
						   Gia=p.Gia,

					   };
			return divu;
		}
	}
}
