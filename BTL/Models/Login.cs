using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class Login
{
    public int Id { get; set; }

    public string? MaNv { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }
}
