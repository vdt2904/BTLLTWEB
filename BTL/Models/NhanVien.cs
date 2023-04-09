using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models;

public partial class NhanVien
{
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "Chỉ được nhập 6 ký tự")]
    public string MaNv { get; set; } = null!;
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    public string? TenNv { get; set; }

    public string? GioiTinh { get; set; }
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Vui lòng nhập giá trị số.")]
    public string? Cccd { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? NgaySinh { get; set; }

    public string? DiaChi { get; set; }
    [RegularExpression(@"^\d+$", ErrorMessage = "Vui lòng nhập giá trị số.")]
    public string? Sdt { get; set; }

    public string? ChucVu { get; set; }
    [RegularExpression(@"^.*\.(jpg|jpeg|png|gif)$", ErrorMessage = "Hãy nhập đúng file ảnh .(jpg|jpeg|png|gif) ")]
    public string? Anh { get; set; }
    [NotMapped]
    public virtual ICollection<Blog> Blogs { get; } = new List<Blog>();

    public virtual ICollection<HoaDon> HoaDons { get; } = new List<HoaDon>();
    [NotMapped]
    public virtual ICollection<Login> Logins { get; } = new List<Login>();
}
