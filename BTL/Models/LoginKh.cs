using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class LoginKh
{
    public int Id { get; set; }

    public string? MaKh { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual KhachHang? MaKhNavigation { get; set; }
}
