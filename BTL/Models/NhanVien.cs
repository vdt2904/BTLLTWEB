using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class NhanVien
{
    public string MaNv { get; set; } = null!;

    public string? TenNv { get; set; }

    public string? GioiTinh { get; set; }

    public string? Cccd { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? DiaChi { get; set; }

    public string? Sdt { get; set; }

    public string? ChucVu { get; set; }

    public string? Anh { get; set; }

    public virtual ICollection<Blog> Blogs { get; } = new List<Blog>();

    public virtual ICollection<HoaDon> HoaDons { get; } = new List<HoaDon>();

    public virtual Login? Login { get; set; }
}
