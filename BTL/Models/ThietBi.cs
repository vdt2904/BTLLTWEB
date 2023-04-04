using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class ThietBi
{
    public string MaTb { get; set; } = null!;

    public string? TenTb { get; set; }

    public double? Gia { get; set; }

    public string? Anh { get; set; }

    public virtual ICollection<SuDungThietBi> SuDungThietBis { get; } = new List<SuDungThietBi>();
}
