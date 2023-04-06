using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class SuDungThietBi
{
    public string MaTb { get; set; } = null!;

    public string MaPhong { get; set; } = null!;
    [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng lớn hơn 0")]
    public int? SoLuong { get; set; }

    public string? TinhTrang { get; set; }

    public virtual Phong? MaPhongNavigation { get; set; }

    public virtual ThietBi? MaTbNavigation { get; set; }
}
