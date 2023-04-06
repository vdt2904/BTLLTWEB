using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class ThietBi
{
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "Chỉ được nhập 6 ký tự")]
    public string MaTb { get; set; } = null!;
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    public string? TenTb { get; set; }
    [RegularExpression(@"^\d+$", ErrorMessage = "Vui lòng nhập giá trị số.")]
    [Range(0, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn 0.")]
    public double? Gia { get; set; }

    public string? Anh { get; set; }

    public virtual ICollection<SuDungThietBi> SuDungThietBis { get; } = new List<SuDungThietBi>();
}
