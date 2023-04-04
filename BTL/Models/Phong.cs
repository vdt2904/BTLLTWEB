using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class Phong
{
    public string MaPhong { get; set; } = null!;

    public string? TenPhong { get; set; }

    public string? TinhTrang { get; set; }

    public string? MaLp { get; set; }

    public string? Anh { get; set; }

    public virtual ICollection<DatPhong> DatPhongs { get; } = new List<DatPhong>();

    public virtual LoaiPhong? MaLpNavigation { get; set; }

    public virtual ICollection<SuDungThietBi> SuDungThietBis { get; } = new List<SuDungThietBi>();
}
