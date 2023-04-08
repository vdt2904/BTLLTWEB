using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTL.Models;
using BTL.Models.RoomModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BTL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomAPIController : ControllerBase
    {
        QlkhachSanAspContext db = new QlkhachSanAspContext();
        [HttpGet]

        public IEnumerable<Room> GetAllRooms()
        {
            var room = from p in db.Phongs
                       join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
                       into temp
                       from t in temp.DefaultIfEmpty()
                       select new Room()
                       {
                           MaPhong = p.MaPhong,
                           TenPhong = p.TenPhong,
                           TinhTrang = p.TinhTrang,
                           MaLp = p.MaLp,
                           Anh = p.Anh,
                           LoaiPhong1 = t.LoaiPhong1,
                           SoNguoiToiDa = t.SoNguoiToiDa,
                           Gia = t.Gia

                       };
            return room;    
        }

        [HttpGet("{maloai}")]
        public IEnumerable<Room> GetRoomsByCategory(string maloai)
        {
            var room = from p in db.Phongs
                       join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp where lp.MaLp == maloai
                       select new Room()
                       {
                           MaPhong = p.MaPhong,
                           TenPhong = p.TenPhong,
                           TinhTrang = p.TinhTrang,
                           MaLp = p.MaLp,
                           Anh = lp.Anh,
                           LoaiPhong1 = lp.LoaiPhong1,
                           SoNguoiToiDa = lp.SoNguoiToiDa,
                           Gia = lp.Gia
                       };
            return room;
        }
    }
}
