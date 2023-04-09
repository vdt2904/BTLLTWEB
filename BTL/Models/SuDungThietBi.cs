using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class SuDungThietBi
{
    public string MaTb { get; set; } = null!;

    public string MaPhong { get; set; } = null!;
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng lớn hơn 0")]
    public int? SoLuong { get; set; }

    public string? TinhTrang { get; set; }
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? NgaySD { get; set; }
    public virtual Phong? MaPhongNavigation { get; set; }   
    public virtual ThietBi? MaTbNavigation { get; set; }
}
