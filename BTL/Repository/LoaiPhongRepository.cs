using BTL.Models;

namespace BTL.Repository
{
	public class LoaiPhongRepository : ILoaiPhongRepository
	{

		private readonly QlkhachSanAspContext _context;
		public LoaiPhongRepository(QlkhachSanAspContext context)
		{
			_context = context;
		}
		public LoaiPhong Add(LoaiPhong loaiPhong)
		{
			_context.LoaiPhongs.Add(loaiPhong);
			_context.SaveChanges();
			return loaiPhong;
		}

		public LoaiPhong Delete(string maloai)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<LoaiPhong> GetAll()
		{
			return _context.LoaiPhongs;
		}

		public LoaiPhong GetLoaiPhong(string maloai)
		{
			return _context.LoaiPhongs.Find(maloai);
		}

		public LoaiPhong Update(LoaiPhong loaiPhong)
		{
			_context.Update(loaiPhong);
			_context.SaveChanges();
			return loaiPhong;
		}
	}
}
