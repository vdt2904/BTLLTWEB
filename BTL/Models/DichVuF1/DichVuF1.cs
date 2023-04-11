namespace BTL.Models.DichVuF1
{
	public class DichVuF1
	{
		public string MaDv { get; set; } = null!;

		public string? TenDv { get; set; }

		public double? Gia { get; set; }

		public string? Anh { get; set; }

		public virtual ICollection<SuDungDichVu> SuDungDichVus { get; } = new List<SuDungDichVu>();
	}
}
