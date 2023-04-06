using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class Blog
{
    public string Idblog { get; set; } = null!;

    public string? MaNv { get; set; }

    public string? Anh { get; set; }

    public string? TieuDe { get; set; }

    public string? ThongTin { get; set; }

    public DateTime? NgayDang { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }
}
