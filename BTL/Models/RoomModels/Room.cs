namespace BTL.Models.RoomModels
{
    public class Room
    {
        public string MaPhong { get; set; } = null!;

        public string? TenPhong { get; set; }

        public string? TinhTrang { get; set; }

        public string? MaLp { get; set; }

        public string? Anh { get; set; }

        public string? LoaiPhong1 { get; set; }

        public int? SoNguoiToiDa { get; set; }

        public double? Gia { get; set; }
    }
}
