using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class LoaiPhong
{
    public string MaLp { get; set; } = null!;

    public string? LoaiPhong1 { get; set; }

    public int? SoNguoiToiDa { get; set; }

    public double? Gia { get; set; }

    public string? Anh { get; set; }

    public virtual ICollection<Phong> Phongs { get; } = new List<Phong>();
}
