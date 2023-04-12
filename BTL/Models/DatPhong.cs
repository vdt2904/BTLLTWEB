using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class DatPhong
{
    public string MaPhong { get; set; } = null!;

    public string SoHoaDon { get; set; } = null!;
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? NgayDen { get; set; }
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? NgayDi { get; set; }
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng lớn hơn 0")]
    public int? SoNguoi { get; set; }

    public virtual Phong? MaPhongNavigation { get; set; }

    public virtual HoaDon? SoHoaDonNavigation { get; set; } 
}
