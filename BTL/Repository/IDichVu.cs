using BTL.Models;

namespace BTL.Repository
{
	public interface IDichVu
	{
		DichVu Add(IDichVu v);
		DichVu Update(IDichVu v);
		DichVu Delete(string madv);
		DichVu Get(string madv);
		IEnumerable<DichVu> GetAll();
	}
}
