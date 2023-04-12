using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class DatPhong
{
    [Required]
    public string MaPhong { get; set; } = null!;
    [Required]
    public string SoHoaDon { get; set; } = null!;
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? NgayDen { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? NgayDi { get; set; }

    public int? SoNguoi { get; set; }

    public virtual Phong? MaPhongNavigation { get; set; }

    public virtual HoaDon? SoHoaDonNavigation { get; set; }
}
