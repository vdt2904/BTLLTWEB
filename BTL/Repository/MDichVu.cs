using BTL.Models;

namespace BTL.Repository
{
	public class MDichVu : IDichVu
	{
		private readonly QlkhachSanAspContext _context;
		public MDichVu(QlkhachSanAspContext context)
		{
			_context = context;
		}
		public DichVu Add(IDichVu v)
		{
			throw new NotImplementedException();
		}

		public DichVu Delete(string madv)
		{
			throw new NotImplementedException();
		}

		public DichVu Get(string madv)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<DichVu> GetAll()
		{
			return _context.DichVus;
		}

		public DichVu Update(IDichVu v)
		{
			throw new NotImplementedException();
		}
	}
}
