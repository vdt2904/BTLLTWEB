using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class DatPhong
{
    public string MaPhong { get; set; } = null!;

    public string SoHoaDon { get; set; } = null!;

    public DateTime? NgayDen { get; set; }

    public DateTime? NgayDi { get; set; }

    public int? SoNguoi { get; set; }

    public virtual Phong MaPhongNavigation { get; set; } = null!;

    public virtual HoaDon SoHoaDonNavigation { get; set; } = null!;
}
