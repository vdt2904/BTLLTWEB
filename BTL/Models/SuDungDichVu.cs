using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class SuDungDichVu
{
    public string MaDv { get; set; } = null!;

    public string SoHoaDon { get; set; } = null!;
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng lớn hơn 0")]
    public int? SoLuong { get; set; }

    public DateTime? NgayMua { get; set; }

    public virtual DichVu? MaDvNavigation { get; set; } 

    public virtual HoaDon? SoHoaDonNavigation { get; set; }
}
