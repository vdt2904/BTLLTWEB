using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class SuDungThietBi
{
    public string MaTb { get; set; } = null!;

    public string MaPhong { get; set; } = null!;

    public int? SoLuong { get; set; }

    public string? TinhTrang { get; set; }

    public virtual Phong MaPhongNavigation { get; set; } = null!;

    public virtual ThietBi MaTbNavigation { get; set; } = null!;
}
