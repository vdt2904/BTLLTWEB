using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class LoaiPhong
{
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "Chỉ được nhập 6 ký tự")]
    public string MaLp { get; set; } = null!;
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    public string? LoaiPhong1 { get; set; }
    [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số")]
    [Range(1, 6, ErrorMessage = "Số người trong khoảng từ {0} đến {1}")]
    public int? SoNguoiToiDa { get; set; }
    [RegularExpression(@"^\d+$", ErrorMessage = "Vui lòng nhập giá trị số.")]
    [Range(100000, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn 100000.")]
    public double? Gia { get; set; }
    [RegularExpression(@"^.*\.(jpg|jpeg|png|gif)$", ErrorMessage = "Hãy nhập đúng file ảnh .(jpg|jpeg|png|gif) ")]
    public string? Anh { get; set; }

    public virtual ICollection<Phong> Phongs { get; } = new List<Phong>();
}
