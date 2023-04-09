using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class Phong
{
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage ="Không được để trống")]
    [StringLength(maximumLength:6,MinimumLength =6,ErrorMessage ="Chỉ được nhập 6 ký tự")]
    public string MaPhong { get; set; } = null!;
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    public string? TenPhong { get; set; }

    public string? TinhTrang { get; set; }
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    public string? MaLp { get; set; }
    [RegularExpression(@"^.*\.(jpg|jpeg|png|gif)$", ErrorMessage = "Hãy nhập đúng file ảnh .(jpg|jpeg|png|gif) ")]
    public string? Anh { get; set; }

    public virtual ICollection<DatPhong> DatPhongs { get; } = new List<DatPhong>();

    public virtual LoaiPhong? MaLpNavigation { get; set; }

    public virtual ICollection<SuDungThietBi> SuDungThietBis { get; } = new List<SuDungThietBi>();
}
