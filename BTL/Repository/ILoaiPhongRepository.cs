using BTL.Models;

namespace BTL.Repository
{
	public interface ILoaiPhongRepository
	{
		LoaiPhong Add(LoaiPhong loaiPhong);
		LoaiPhong Update(LoaiPhong loaiPhong);
		LoaiPhong Delete(String maloai);
		LoaiPhong GetLoaiPhong(String maloai);

		IEnumerable<LoaiPhong> GetAll();

	}
}
