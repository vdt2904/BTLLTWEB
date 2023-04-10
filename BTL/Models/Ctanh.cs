using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class Ctanh
{
    public string TenAnh { get; set; } = null!;

    public string? MaLp { get; set; }

    public virtual LoaiPhong? MaLpNavigation { get; set; }
}
