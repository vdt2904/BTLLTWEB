using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class DichVu
{
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "Chỉ được nhập 6 ký tự")]
    public string MaDv { get; set; } = null!;
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    public string? TenDv { get; set; }
    [RegularExpression(@"^\d+$", ErrorMessage = "Vui lòng nhập giá trị số.")]
    [Range(0, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn 0.")]
    public double? Gia { get; set; }
    [RegularExpression(@"^.*\.(jpg|jpeg|png|gif)$", ErrorMessage = "Hãy nhập đúng file ảnh .(jpg|jpeg|png|gif) ")]
    public string? Anh { get; set; }

    public string? GioiThieu { get; set; }

    public virtual ICollection<SuDungDichVu> SuDungDichVus { get; } = new List<SuDungDichVu>();
}
