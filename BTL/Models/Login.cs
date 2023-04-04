using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class Login
{
    public string MaNv { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
