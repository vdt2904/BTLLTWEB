using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class KhachHang
{
    public string MaKh { get; set; } = null!;

    public string? TenKh { get; set; }

    public string? GioiTinh { get; set; }

    public string? Cccd { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? DiaChi { get; set; }

    public string? Sdt { get; set; }

    public string? LoaiKhachHang { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; } = new List<HoaDon>();
}
