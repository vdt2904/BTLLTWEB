using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class LoginKh
{
    public int Id { get; set; }

    public string? MaKh { get; set; }
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    public string? Username { get; set; }
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Không được để trống")]
    public string? Password { get; set; }

    public virtual KhachHang? MaKhNavigation { get; set; }
}
