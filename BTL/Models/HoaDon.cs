using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class HoaDon
{
    public string SoHoaDon { get; set; } = null!;

    public DateTime? NgayThanhToan { get; set; }

    public string? MaNv { get; set; }

    public string? MaKh { get; set; }

    public virtual ICollection<DatPhong> DatPhongs { get; } = new List<DatPhong>();

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }

    public virtual ICollection<SuDungDichVu> SuDungDichVus { get; } = new List<SuDungDichVu>();
}
